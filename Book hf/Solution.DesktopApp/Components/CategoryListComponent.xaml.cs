namespace Solution.DesktopApp.Components;

public partial class CategoryListComponent : ContentView
{
    public static readonly BindableProperty CategoryProperty = BindableProperty.Create(
         propertyName: nameof(Category),
         returnType: typeof(CategoryModel),
         declaringType: typeof(CategoryListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.OneWay
    );

    public CategoryModel Category
    {
        get => (CategoryModel)GetValue(CategoryProperty);
        set => SetValue(CategoryProperty, value);
    }

    public static readonly BindableProperty DeleteCommandProperty = BindableProperty.Create(
         propertyName: nameof(DeleteCommand),
         returnType: typeof(IAsyncRelayCommand),
         declaringType: typeof(CategoryListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.OneWay
    );

    public IAsyncRelayCommand DeleteCommand
    {
        get => (IAsyncRelayCommand)GetValue(DeleteCommandProperty);
        set => SetValue(DeleteCommandProperty, value);
    }

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
         propertyName: nameof(CommandParameter),
         returnType: typeof(string),
         declaringType: typeof(CategoryListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.TwoWay
        );

    public string CommandParameter
    {
        get => (string)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public IAsyncRelayCommand EditCommand => new AsyncRelayCommand(OnEditAsync);

    public CategoryListComponent()
	{
		InitializeComponent();
	}

    private async Task OnEditAsync()
    {
        ShellNavigationQueryParameters navigationQueryParameter = new ShellNavigationQueryParameters
        {
            { "Category", this.Category}
        };

        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(AddCategoryView.Name, navigationQueryParameter);
    }
}