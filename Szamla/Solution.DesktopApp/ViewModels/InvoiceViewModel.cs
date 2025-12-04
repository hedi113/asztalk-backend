

namespace Solution.DesktopApp.ViewModels;

public partial class InvoiceViewModel(IInvoiceService invoiceService, IInvoiceItemService invoiceItemService, AppDbContext dbContext) : InvoiceModel
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
    public ICommand OnAddInvoiceCommand {  get; private set; }
    public IAsyncRelayCommand EditCommand => new AsyncRelayCommand<InvoiceItemModel>(OnUpdInvoiceItemAsync);
    public IRelayCommand DeleteCommand => new RelayCommand<InvoiceItemModel>(OnRemoveInvoiceItem);
    #endregion

    private InvoiceModelValidator invoiceValidator => new InvoiceModelValidator(null);

    private InvoiceItemModelValidator invoiceItemValidator => new InvoiceItemModelValidator(null);

    [ObservableProperty]
    private ValidationResult validationResult = new ValidationResult();


    [ObservableProperty]
    private InvoiceItemModel invoiceItem = new InvoiceItemModel();


    private async Task OnAppearingkAsync()
    {
        OnAddInvoiceCommand = new Command(async () => await OnSaveInvoiceAsync(), () => InvoiceItems != null);
    }

    private async Task OnDisappearingAsync()
    { }

    private async Task OnSaveInvoiceItemAsync()
    {
        this.ValidationResult = await invoiceItemValidator.ValidateAsync(InvoiceItem);

        if (!ValidationResult.IsValid)
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
    }

    private async Task OnSaveInvoiceAsync()
    {
        ((Command)OnAddInvoiceCommand).ChangeCanExecute();


        this.ValidationResult = await invoiceValidator.ValidateAsync(this);

        if (!ValidationResult.IsValid)
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
    }

    private void OnRemoveInvoiceItem(InvoiceItemModel model)
    {
        this.InvoiceItems.Remove(model);
    }

    private async Task OnUpdInvoiceItemAsync(InvoiceItemModel model)
    {
        InvoiceItem = model;
        InvoiceItems.Remove(model);
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

        ValidationResult.Errors.Remove(ValidationResult.Errors.FirstOrDefault(x => x.PropertyName == propertyName));

        ValidationResult.Errors.Remove(ValidationResult.Errors.FirstOrDefault(x => x.PropertyName == InvoiceModelValidator.GlobalProperty));
        ValidationResult.Errors.AddRange(result.Errors);

        OnPropertyChanged(nameof(ValidationResult));
    }

    private async Task OnValidateInvoiceItemAsync(string propertyName)
    {
        var result = await invoiceItemValidator.ValidateAsync(InvoiceItem, options => options.IncludeProperties(propertyName));

        ValidationResult.Errors.Remove(ValidationResult.Errors.FirstOrDefault(x => x.PropertyName == propertyName));

        ValidationResult.Errors.Remove(ValidationResult.Errors.FirstOrDefault(x => x.PropertyName == InvoiceItemModelValidator.GlobalProperty));
        ValidationResult.Errors.AddRange(result.Errors);

        OnPropertyChanged(nameof(ValidationResult));
    }
}
