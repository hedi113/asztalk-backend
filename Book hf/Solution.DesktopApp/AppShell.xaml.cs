namespace Solution.DesktopApp;

public partial class AppShell : Shell
{
    public AppShellViewModel ViewModel => this.BindingContext as AppShellViewModel;

    public AppShell(AppShellViewModel viewModel)
    {
        this.BindingContext = viewModel;

        InitializeComponent();

        ConfigureShellNavigation();
    }

    private static void ConfigureShellNavigation()
    {
        Routing.RegisterRoute(MainView.Name, typeof(MainView));
        Routing.RegisterRoute(BookListView.Name, typeof(BookListView));
        Routing.RegisterRoute(AuthorListView.Name, typeof(AuthorListView));
        Routing.RegisterRoute(CategoryListView.Name, typeof(CategoryListView));
        Routing.RegisterRoute(AddBookView.Name, typeof(AddBookView));
        Routing.RegisterRoute(AddCategoryView.Name, typeof(AddCategoryView));
        Routing.RegisterRoute(AddAuthorView.Name, typeof(AddAuthorView));
    }
}
