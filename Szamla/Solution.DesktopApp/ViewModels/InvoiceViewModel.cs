

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
    public IAsyncRelayCommand OnAddInvoiceItemCommand => new AsyncRelayCommand(OnAddInvoiceItemAsync);
    public IAsyncRelayCommand OnAddInvoiceCommand => new AsyncRelayCommand(OnAddInvoiceAsync);
    #endregion

    private InvoiceModelValidator invoiceValidator => new InvoiceModelValidator(null);

    private InvoiceItemModelValidator invoiceItemValidator => new InvoiceItemModelValidator(null);

    [ObservableProperty]
    private ValidationResult validationResult = new ValidationResult();

    private delegate Task ButtonActionDelagate();
    private ButtonActionDelagate asyncButtonActionAddInvoiceItem;
    private ButtonActionDelagate asyncButtonActionAddInvoice;
    private ButtonActionDelagate asyncButtonActionUpdateInvoiceItem;


    [ObservableProperty]
    private InvoiceItemModel invoiceItem = new InvoiceItemModel();


    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        bool invoiceHasValue = query.TryGetValue("InvoiceItem", out object result);

        asyncButtonActionAddInvoiceItem = OnSaveInvoiceItemAsync;
        asyncButtonActionAddInvoice = OnSaveInvoiceAsync;



            asyncButtonActionUpdateInvoiceItem = OnUpdateInvoiceItemAsync;
    }

    private async Task OnAppearingkAsync()
    {
    }

    private async Task OnDisappearingAsync()
    { }

    private async Task OnAddInvoiceItemAsync() => await asyncButtonActionAddInvoiceItem();
    private async Task OnAddInvoiceAsync() => await asyncButtonActionAddInvoice();

    private async Task OnSaveInvoiceItemAsync()
    {
        this.ValidationResult = await invoiceItemValidator.ValidateAsync(InvoiceItem);

        if(!ValidationResult.IsValid)
        {
            return;
        }

        var result = await invoiceItemService.CreateAsync(InvoiceItem);
        var message = result.IsError ? result.FirstError.Description : "Invoice item saved";
        var title = result.IsError ? "Error" : "Information";
        
        if(!result.IsError)
        {
            ClearInvoiceItemForm();
        } 

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private async Task OnSaveInvoiceAsync()
    {
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
    }

    private async Task OnUpdateInvoiceAsync()
    {
        this.ValidationResult = await invoiceValidator.ValidateAsync(this);

        if (!ValidationResult.IsValid)
        {
            return;
        }

        var result = await invoiceService.UpdateAsync(this);
        var message = result.IsError ? result.FirstError.Description : "Invoice updated";
        var title = result.IsError ? "Error" : "Information";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private async Task OnUpdateInvoiceItemAsync()
    {


        this.ValidationResult = await invoiceItemValidator.ValidateAsync(InvoiceItem);

        if (!ValidationResult.IsValid)
        {
            return;
        }

        var result = await invoiceItemService.UpdateAsync(InvoiceItem);
        var message = result.IsError ? result.FirstError.Description : "Invoice item updated";
        var title = result.IsError ? "Error" : "Information";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
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
