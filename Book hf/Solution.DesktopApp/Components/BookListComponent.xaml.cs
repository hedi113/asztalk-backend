namespace Solution.DesktopApp.Components;

public partial class BookListComponent : ContentView
{
    public static readonly BindableProperty BookProperty = BindableProperty.Create(
         propertyName: nameof(Book),
         returnType: typeof(BookModel),
         declaringType: typeof(BookListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.OneWay
    );

    public BookModel Book
    {
        get => (BookModel)GetValue(BookProperty);
        set => SetValue(BookProperty, value);
    }

    public static readonly BindableProperty DeleteCommandProperty = BindableProperty.Create(
         propertyName: nameof(DeleteCommand),
         returnType: typeof(IAsyncRelayCommand),
         declaringType: typeof(BookListComponent),
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
         declaringType: typeof(BookListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.TwoWay
        );

    public string CommandParameter
    {
        get => (string)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public IAsyncRelayCommand EditCommand => new AsyncRelayCommand(OnEditAsync);

    public BookListComponent()
	{
		InitializeComponent();
	}

    private async Task OnEditAsync()
    {
        ShellNavigationQueryParameters navigationQueryParameter = new ShellNavigationQueryParameters
        {
            { "Book", this.Book}
        };

        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(AddBookView.Name, navigationQueryParameter);
    }
}