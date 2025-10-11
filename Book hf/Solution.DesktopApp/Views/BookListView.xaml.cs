namespace Solution.DesktopApp.Views;

public partial class BookListView : ContentPage
{
	public BookListViewModel ViewModel => this.BindingContext as BookListViewModel;

	public static string Name => nameof(BookListView);

    public BookListView(BookListViewModel viewModel)
	{
		this.BindingContext = viewModel;

		InitializeComponent();
	}
}