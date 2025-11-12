//namespace Solution.DesktopApp.Components;

//public partial class AuthorListComponent : ContentView
//{
//    public static readonly BindableProperty AuthorProperty = BindableProperty.Create(
//         propertyName: nameof(Author),
//         returnType: typeof(AuthorModel),
//         declaringType: typeof(AuthorListComponent),
//         defaultValue: null,
//         defaultBindingMode: BindingMode.OneWay
//    );

//    public AuthorModel Author
//    {
//        get => (AuthorModel)GetValue(AuthorProperty);
//        set => SetValue(AuthorProperty, value);
//    }

//    public static readonly BindableProperty DeleteCommandProperty = BindableProperty.Create(
//         propertyName: nameof(DeleteCommand),
//         returnType: typeof(IAsyncRelayCommand),
//         declaringType: typeof(AuthorListComponent),
//         defaultValue: null,
//         defaultBindingMode: BindingMode.OneWay
//    );

//    public IAsyncRelayCommand DeleteCommand
//    {
//        get => (IAsyncRelayCommand)GetValue(DeleteCommandProperty);
//        set => SetValue(DeleteCommandProperty, value);
//    }

//    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
//         propertyName: nameof(CommandParameter),
//         returnType: typeof(string),
//         declaringType: typeof(AuthorListComponent),
//         defaultValue: null,
//         defaultBindingMode: BindingMode.TwoWay
//        );

//    public string CommandParameter
//    {
//        get => (string)GetValue(CommandParameterProperty);
//        set => SetValue(CommandParameterProperty, value);
//    }

//    public IAsyncRelayCommand EditCommand => new AsyncRelayCommand(OnEditAsync);

//    public AuthorListComponent()
//	{
//		InitializeComponent();
//	}

//    private async Task OnEditAsync()
//    {
//        ShellNavigationQueryParameters navigationQueryParameter = new ShellNavigationQueryParameters
//        {
//            { "Author", this.Author}
//        };

//        Shell.Current.ClearNavigationStack();
//        await Shell.Current.GoToAsync(AddAuthorView.Name, navigationQueryParameter);
//    }
//}