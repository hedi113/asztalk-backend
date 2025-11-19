

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
    public IAsyncRelayCommand OnAddOrUpdateCommand => new AsyncRelayCommand(OnAddOrUpdateAsync);
    #endregion

    private InvoiceModelValidator invoiceValidator => new InvoiceModelValidator(null);

    private InvoiceItemModelValidator invoiceItemValidator => new InvoiceItemModelValidator(null);

    [ObservableProperty]
    private ValidationResult validationResult = new ValidationResult();

    private delegate Task ButtonActionDelagate();
    private ButtonActionDelagate asyncButtonAction;


    [ObservableProperty]
    private InvoiceItemModel invoiceItem;


    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        bool invoiceItemHasValue = query.TryGetValue("InvoiceItem", out object result);

        if (!invoiceItemHasValue)
        {
            asyncButtonAction = OnSaveInvoiceItemAsync;
            return;
        }

        if (asyncButtonAction == null)
        {
            InvoiceItemModel invoiceItem = result as InvoiceItemModel;

            InvoiceItem.UnitPrice = invoiceItem.UnitPrice;
            InvoiceItem.Quantity = invoiceItem.Quantity;
            InvoiceItem.Name = invoiceItem.Name; 

            asyncButtonAction = OnUpdateInvoiceItemAsync;
        }
        bool invoiceHasValue = query.TryGetValue("Invoice", out object res);

        if (!invoiceHasValue)
        {
            asyncButtonAction = OnSaveInvoiceAsync; 
            return;
        }

        InvoiceModel invoice = result as InvoiceModel;

        this.CreationDate = invoice.CreationDate;
        this.InvoiceItems = invoice.InvoiceItems;
        this.SumOfInvoiceItemValues = invoice.SumOfInvoiceItemValues;
        this.InvoiceNumber = invoice.InvoiceNumber;

        asyncButtonAction = OnUpdateInvoiceAsync;

    }

    private async Task OnAppearingkAsync()
    {
    }

    private async Task OnDisappearingAsync()
    { }

    private async Task OnAddOrUpdateAsync() => await asyncButtonAction();

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
