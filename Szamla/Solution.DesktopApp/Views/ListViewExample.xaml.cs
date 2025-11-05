namespace Solution.DesktopApp.Views;

public partial class AuthorListView : ContentPage
{
	public AuthorListViewModel ViewModel => this.BindingContext as AuthorListViewModel;

	public static string Name => nameof(AuthorListView);

    public AuthorListView(AuthorListViewModel viewModel)
	{
		this.BindingContext = viewModel;

		InitializeComponent();
	}
}