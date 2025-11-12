namespace Solution.DesktopApp.ViewModels;

public partial class InvoiceItemViewModel(IInvoiceItemService invoiceItemService, 
    AppDbContext dbContext) : InvoiceItemModel, IQueryAttributable
{
    #region life cycle commands
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingkAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
    #endregion

    #region validation
    public IRelayCommand ValidateCommand => new AsyncRelayCommand<string>(OnValidateAsync);
    #endregion

    #region event commands
    public IAsyncRelayCommand SubmitCommand => new AsyncRelayCommand(OnSubmitAsync);
    #endregion

    private InvoiceItemModelValidator validator => new InvoiceItemModelValidator(null);

    [ObservableProperty]
    private ValidationResult validationResult = new ValidationResult();

    private delegate Task ButtonActionDelegate();
    private ButtonActionDelegate asyncButtonAction;

    public async void ApplyQuieryAttributes(IDictionary<string, object> query)
    {
        bool hasValue = query.TryGetValue("InvoiceItem", out object result);

        if (!hasValue)
        {
            asyncButtonAction = OnSaveAsync;
            return;
        }

        InvoiceItemModel invoiceItem = result as InvoiceItemModel;

        this.Id = invoiceItem.Id;
        this.Name = invoiceItem.Name;
        this.UnitPrice = invoiceItem.UnitPrice;
        this.Quantity = invoiceItem.Quantity;

        asyncButtonAction = OnUpdateAsync;
    }

    private async Task OnAppearingkAsync()
    {
    }

    private async Task OnDisappearingAsync()
    { }

    private async Task OnSubmitAsync() => await asyncButtonAction();

    private async Task OnSaveAsync()
    {
        this.ValidationResult = await validator.ValidateAsync(this);

        if(!ValidationResult.IsValid)
        {
            return;
        }

        var result = await invoiceItemService.CreateAsync(this);
        var message = result.IsError ? result.FirstError.Description : "Invoice item saved.";
        var title = result.IsError ? "Error" : "Information";

        if (!result.IsError)
        {
            ClearForm();
        }

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private async Task OnUpdateAsync()
    {
        this.ValidationResult = await validator.ValidateAsync(this);

        if (!ValidationResult.IsValid)
        {
            return;
        }

        var result = await invoiceItemService.UpdateAsync(this);

        var message = result.IsError ? result.FirstError.Description : "Invoice item updated.";
        var title = result.IsError ? "Error" : "Information";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private void ClearForm()
    {
        this.Name = null;
        this.UnitPrice = null;
        this.Quantity = null;
    }

    private async Task OnValidateAsync(string propertyName)
    {
        var result = await validator.ValidateAsync(this, options => options.IncludeProperties(propertyName));

        ValidationResult.Errors.Remove(ValidationResult.Errors.FirstOrDefault(x => x.PropertyName == propertyName));

        ValidationResult.Errors.Remove(ValidationResult.Errors.FirstOrDefault(x => x.PropertyName == InvoiceItemModelValidator.GlobalProperty));
        ValidationResult.Errors.AddRange(result.Errors);

        OnPropertyChanged(nameof(ValidationResult));
    }
}
