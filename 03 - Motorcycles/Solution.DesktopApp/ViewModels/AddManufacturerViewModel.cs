using Solution.Database.Migrations;
using Solution.Services;

namespace Solution.DesktopApp.ViewModels;

public partial class AddManufacturerViewModel(IManufacturerService manufacturerService,
    AppDbContext dbContext) : ManufacturerModel, IQueryAttributable
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

    private ManufacturerModelValidator validator => new ManufacturerModelValidator(null);

    [ObservableProperty]
    private ValidationResult validationResult = new ValidationResult();

    [ObservableProperty]
    private string title;

    private delegate Task ButtonActionDelagate();
    private ButtonActionDelagate asyncButtonAction;

    [ObservableProperty]
    private string name;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        bool hasValue = query.TryGetValue("Manufacturer", out object result);

        if (!hasValue)
        {
            asyncButtonAction = OnSaveAsync;
            Title = "Add new manufacturer";
            return;
        }

        ManufacturerModel manufacturer = result as ManufacturerModel;

        this.Id = manufacturer.Id;
        this.Name = manufacturer.Name;

        asyncButtonAction = OnUpdateAsync;
        Title = "Update manufacturer";
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

        if (!ValidationResult.IsValid)
        {
            return;
        }

        var result = await manufacturerService.CreateAsync(this);
        var message = result.IsError ? result.FirstError.Description : "Manufacturer saved.";
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

        var result = await manufacturerService.UpdateAsync(this);

        var message = result.IsError ? result.FirstError.Description : "Type updated.";
        var title = result.IsError ? "Error" : "Information";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private void ClearForm()
    {
        this.Name = null;
    }

    private async Task OnValidateAsync(string propertyName)
    {
        var result = await validator.ValidateAsync(this, options => options.IncludeProperties(propertyName));

        ValidationResult.Errors.Remove(ValidationResult.Errors.FirstOrDefault(x => x.PropertyName == propertyName));

        ValidationResult.Errors.Remove(ValidationResult.Errors.FirstOrDefault(x => x.PropertyName == ManufacturerModelValidator.GlobalProperty));
        ValidationResult.Errors.AddRange(result.Errors);

        OnPropertyChanged(nameof(ValidationResult));
    }
}
