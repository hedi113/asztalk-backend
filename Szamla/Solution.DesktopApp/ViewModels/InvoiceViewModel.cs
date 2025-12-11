


namespace Solution.DesktopApp.ViewModels;


public partial class InvoiceViewModel(IInvoiceService invoiceService, IInvoiceItemService invoiceItemService, AppDbContext dbContext) : InvoiceModel, IQueryAttributable
{
    #region life cycle commands
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingkAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
    #endregion

    #region validation
    public IRelayCommand ValidateInvoiceCommand => new AsyncRelayCommand<string>(OnValidateInvoiceAsync);
    public IRelayCommand ValidateInvoiceItemCommand => new AsyncRelayCommand<string>(OnValidateInvoiceItemAsync);
    #endregion

    #region event commands
    public IAsyncRelayCommand OnAddInvoiceItemCommand => new AsyncRelayCommand(OnSaveInvoiceItemAsync);
    public IAsyncRelayCommand OnSaveInvoiceCommand => new AsyncRelayCommand(async () => await OnSaveInvoiceAsync(), () => InvoiceItems?.Count > 0 && InvoiceValidationResult.IsValid);
    public IAsyncRelayCommand EditCommand => new AsyncRelayCommand<InvoiceItemModel>(OnUpdInvoiceItemAsync);
    public IRelayCommand DeleteCommand => new RelayCommand<InvoiceItemModel>(OnRemoveInvoiceItem);
    #endregion

    private InvoiceModelValidator invoiceValidator => new InvoiceModelValidator(null);

    private InvoiceItemModelValidator invoiceItemValidator => new InvoiceItemModelValidator(null);

    [ObservableProperty]
    private ValidationResult invoiceValidationResult = new ValidationResult();

    [ObservableProperty]
    private ValidationResult invoiceItemValidationResult = new ValidationResult();

    [ObservableProperty]
    private InvoiceItemModel invoiceItem = new InvoiceItemModel();


    private async Task OnAppearingkAsync()
    {
        //OnSaveInvoiceCommand.NotifyCanExecuteChanged();
        OnSaveInvoiceCommand.NotifyCanExecuteChanged();
    }

    private async Task OnDisappearingAsync()
    { }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        bool hasValue = query.TryGetValue("Invoice", out object result);

        if(!hasValue)
        {
            return;
        }

        InvoiceModel model = result as InvoiceModel;

        this.Id = model.Id;
        this.InvoiceNumber = model.InvoiceNumber;
        this.CreationDate = model.CreationDate;
        this.SumOfInvoiceItemValues = model.SumOfInvoiceItemValues;
        this.InvoiceItems = model.InvoiceItems;

        OnSaveInvoiceCommand.NotifyCanExecuteChanged();
    }

    private async Task OnSaveInvoiceItemAsync()
    {
        this.InvoiceItemValidationResult = await invoiceItemValidator.ValidateAsync(InvoiceItem);

        if (!InvoiceItemValidationResult.IsValid)
        {
            return;
        }
        var a = InvoiceItems.ToList();
        a.Add(new InvoiceItemModel
        { 
            Name = InvoiceItem.Name,
            Quantity = InvoiceItem.Quantity,
            UnitPrice = InvoiceItem.UnitPrice,
            Id = InvoiceItem.Id,
            InvoiceId = this.Id
        });
        InvoiceItems = new ObservableCollection<InvoiceItemModel>(a);

        ClearInvoiceItemForm();

        OnSaveInvoiceCommand.NotifyCanExecuteChanged();
    }

    private async Task OnSaveInvoiceAsync()
    {
        this.InvoiceValidationResult = await invoiceValidator.ValidateAsync(this);

        if (!InvoiceValidationResult.IsValid)
        {
            return;
        }

        var result = await invoiceService.CreateAsync(this);
        var message = result.IsError ? result.FirstError.Description : "Invoice saved";
        var title = result.IsError ? "Error" : "Information";

        if (!result.IsError)
        {
            ClearInvoiceForm();
        }

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");

        ClearInvoiceForm();

        OnSaveInvoiceCommand.NotifyCanExecuteChanged();

    }

    private async Task OnUpdateInvoiceAsync()
    {
        this.InvoiceValidationResult = await invoiceValidator.ValidateAsync(this);

        if (!InvoiceValidationResult.IsValid)
        {
            return;
        }

        var result = await invoiceService.UpdateAsync(this);
        var message = result.IsError ? result.FirstError.Description : "Invoice saved";
        var title = result.IsError ? "Error" : "Information";

        if (!result.IsError)
        {
            ClearInvoiceForm();
        }

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");

        ClearInvoiceForm();
        OnSaveInvoiceCommand.NotifyCanExecuteChanged();
    }

    private void OnRemoveInvoiceItem(InvoiceItemModel model)
    {
        this.InvoiceItems.Remove(model);
        OnSaveInvoiceCommand.NotifyCanExecuteChanged();

    }

    private async Task OnUpdInvoiceItemAsync(InvoiceItemModel model)
    {
        InvoiceItem = model;
        InvoiceItems.Remove(model);

        OnSaveInvoiceCommand.NotifyCanExecuteChanged();
    }


    private void ClearInvoiceItemForm()
    {
        InvoiceItem.UnitPrice = null;
        InvoiceItem.Quantity = null;
        InvoiceItem.Name = null;
    }

    private void ClearInvoiceForm()
    {
        this.InvoiceItems.Clear();
        this.InvoiceNumber = null;
        this.CreationDate = DateTime.Now;
        this.SumOfInvoiceItemValues = 0;
    }

    private async Task OnValidateInvoiceAsync(string propertyName)
    {
        var result = await invoiceValidator.ValidateAsync(this, options => options.IncludeProperties(propertyName));

        InvoiceValidationResult.Errors.Remove(InvoiceValidationResult.Errors.FirstOrDefault(x => x.PropertyName == propertyName));

        InvoiceValidationResult.Errors.Remove(InvoiceValidationResult.Errors.FirstOrDefault(x => x.PropertyName == InvoiceModelValidator.GlobalProperty));
        InvoiceValidationResult.Errors.AddRange(result.Errors);

        OnPropertyChanged(nameof(InvoiceValidationResult));
        OnSaveInvoiceCommand.NotifyCanExecuteChanged();
    }

    private async Task OnValidateInvoiceItemAsync(string propertyName)
    {
        var result = await invoiceItemValidator.ValidateAsync(InvoiceItem, options => options.IncludeProperties(propertyName));

        InvoiceItemValidationResult.Errors.Remove(InvoiceItemValidationResult.Errors.FirstOrDefault(x => x.PropertyName == propertyName));

        InvoiceItemValidationResult.Errors.Remove(InvoiceItemValidationResult.Errors.FirstOrDefault(x => x.PropertyName == InvoiceItemModelValidator.GlobalProperty));
        InvoiceItemValidationResult.Errors.AddRange(result.Errors);

        OnPropertyChanged(nameof(InvoiceItemValidationResult));
        OnSaveInvoiceCommand.NotifyCanExecuteChanged();
    }

    
}
