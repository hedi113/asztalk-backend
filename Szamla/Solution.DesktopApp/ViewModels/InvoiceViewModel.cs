namespace Solution.DesktopApp.ViewModels;

public partial class InvoiceViewModel : InvoiceModel, IQueryAttributable
{
    private readonly IInvoiceService invoiceService;
    private readonly IInvoiceItemService invoiceItemService;
    private readonly AppDbContext dbContext;

    #region life cycle commands
    public IAsyncRelayCommand AppearingCommand { get; }
    public IAsyncRelayCommand DisappearingCommand { get; }
    #endregion

    #region validation
    public IRelayCommand ValidateInvoiceCommand { get; }
    public IRelayCommand ValidateInvoiceItemCommand { get; }
    #endregion

    #region event commands
    public IAsyncRelayCommand AddInvoiceItemCommand { get; }
    public IAsyncRelayCommand SubmitCommand { get; }
    public IAsyncRelayCommand EditCommand { get; }
    public IRelayCommand DeleteCommand { get; }
    #endregion

    private readonly InvoiceModelValidator invoiceValidator = new(null);
    private readonly InvoiceItemModelValidator invoiceItemValidator = new(null);

    [ObservableProperty]
    private ValidationResult invoiceValidationResult = new();

    [ObservableProperty]
    private ValidationResult invoiceItemValidationResult = new();

    [ObservableProperty]
    private InvoiceItemModel invoiceItem = new();

    [ObservableProperty]
    private string buttonTitle;

    private bool isEditMode;

    public DateTime MaxDateTime => DateTime.Now;

    public InvoiceViewModel(
        IInvoiceService invoiceService,
        IInvoiceItemService invoiceItemService,
        AppDbContext dbContext)
    {
        this.invoiceService = invoiceService;
        this.invoiceItemService = invoiceItemService;
        this.dbContext = dbContext;

        AppearingCommand = new AsyncRelayCommand(OnAppearingAsync);
        DisappearingCommand = new AsyncRelayCommand(OnDisappearingAsync);

        AddInvoiceItemCommand = new AsyncRelayCommand(OnSaveInvoiceItemAsync);
        SubmitCommand = new AsyncRelayCommand(OnSubmitAsync, CanSaveInvoice);

        EditCommand = new AsyncRelayCommand<InvoiceItemModel>(OnEditInvoiceItemAsync);
        DeleteCommand = new RelayCommand<InvoiceItemModel>(OnRemoveInvoiceItem);

        ValidateInvoiceCommand =
            new AsyncRelayCommand<string>(OnValidateInvoiceAsync);

        ValidateInvoiceItemCommand =
            new AsyncRelayCommand<string>(OnValidateInvoiceItemAsync);

        ButtonTitle = "Számla mentése";
    }

    #region Lifecycle

    private Task OnAppearingAsync()
    {
        SubmitCommand.NotifyCanExecuteChanged();
        return Task.CompletedTask;
    }

    private Task OnDisappearingAsync() => Task.CompletedTask;

    #endregion

    #region Navigation

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue("Invoice", out var result))
        {
            isEditMode = false;
            ButtonTitle = "Számla mentése";
            return;
        }

        var model = (InvoiceModel)result;

        Id = model.Id;
        InvoiceNumber = model.InvoiceNumber;
        CreationDate = model.CreationDate;
        SumOfInvoiceItemValues = model.SumOfInvoiceItemValues;
        InvoiceItems = model.InvoiceItems;

        isEditMode = true;
        ButtonTitle = "Változtatások mentése";

        SubmitCommand.NotifyCanExecuteChanged();
    }

    #endregion

    #region Submit

    private bool CanSaveInvoice() =>
        (InvoiceValidationResult?.IsValid ?? false) &&
        InvoiceItems?.Count > 0;

    private async Task OnSubmitAsync()
    {
        InvoiceValidationResult = await invoiceValidator.ValidateAsync(this);
        if (!InvoiceValidationResult.IsValid)
            return;

        if (isEditMode)
        {
            var result = await invoiceService.UpdateAsync(this);
            await HandleResultAsync(result);
        }
        else
        {
            var result = await invoiceService.CreateAsync(this);
            await HandleResultAsync(result);
        }

        SubmitCommand.NotifyCanExecuteChanged();
    }

    private async Task HandleResultAsync(ErrorOr<Success> result)
    {
        var title = result.IsError ? "Hiba" : "Infó";
        var message = result.IsError
            ? result.FirstError.Description
            : "Számla elmentve.";

        if (!result.IsError)
            ClearInvoiceForm();

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private async Task HandleResultAsync(ErrorOr<InvoiceModel> result)
    {
        var title = result.IsError ? "Hiba" : "Infó";
        var message = result.IsError
            ? result.FirstError.Description
            : "Számla elmentve.";

        if (!result.IsError)
            ClearInvoiceForm();

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }



    #endregion

    #region Invoice Items

    private async Task OnSaveInvoiceItemAsync()
    {
        InvoiceItemValidationResult =
            await invoiceItemValidator.ValidateAsync(InvoiceItem);

        if (!InvoiceItemValidationResult.IsValid)
            return;

        var items = InvoiceItems.ToList();

        items.Add(new InvoiceItemModel
        {
            Name = InvoiceItem.Name,
            Quantity = InvoiceItem.Quantity,
            UnitPrice = InvoiceItem.UnitPrice,
            Id = InvoiceItem.Id,
            InvoiceId = Id
        });

        InvoiceItems = new ObservableCollection<InvoiceItemModel>(items);

        ClearInvoiceItemForm();
        SubmitCommand.NotifyCanExecuteChanged();
    }

    private void OnRemoveInvoiceItem(InvoiceItemModel model)
    {
        InvoiceItems.Remove(model);
        SubmitCommand.NotifyCanExecuteChanged();
    }

    private Task OnEditInvoiceItemAsync(InvoiceItemModel model)
    {
        InvoiceItem = model;
        InvoiceItems.Remove(model);
        SubmitCommand.NotifyCanExecuteChanged();
        return Task.CompletedTask;
    }

    #endregion

    #region Validation

    private async Task OnValidateInvoiceAsync(string propertyName)
    {
        var result = await invoiceValidator.ValidateAsync(
            this,
            o => o.IncludeProperties(propertyName));

        InvoiceValidationResult.Errors.RemoveAll(
            x => x.PropertyName == propertyName ||
                 x.PropertyName == InvoiceModelValidator.GlobalProperty);

        InvoiceValidationResult.Errors.AddRange(result.Errors);

        OnPropertyChanged(nameof(InvoiceValidationResult));
        SubmitCommand.NotifyCanExecuteChanged();
    }

    private async Task OnValidateInvoiceItemAsync(string propertyName)
    {
        var result = await invoiceItemValidator.ValidateAsync(
            InvoiceItem,
            o => o.IncludeProperties(propertyName));

        InvoiceItemValidationResult.Errors.RemoveAll(
            x => x.PropertyName == propertyName ||
                 x.PropertyName == InvoiceItemModelValidator.GlobalProperty);

        InvoiceItemValidationResult.Errors.AddRange(result.Errors);

        OnPropertyChanged(nameof(InvoiceItemValidationResult));
        SubmitCommand.NotifyCanExecuteChanged();
    }

    #endregion

    #region Clear helpers

    private void ClearInvoiceItemForm()
    {
        InvoiceItem = new InvoiceItemModel();
    }

    private void ClearInvoiceForm()
    {
        InvoiceItems.Clear();
        InvoiceNumber = null;
        CreationDate = DateTime.Now;
        SumOfInvoiceItemValues = 0;
        isEditMode = false;
        ButtonTitle = "Számla mentése";
    }

    #endregion
}
