namespace Solution.DesktopApp.Views;

public partial class CategoryListView : ContentPage
{
	public CategoryListViewModel ViewModel => this.BindingContext as CategoryListViewModel;

	public static string Name => nameof(CategoryListView);

    public CategoryListView(CategoryListViewModel viewModel)
	{
		this.BindingContext = viewModel;

		InitializeComponent();
	}
}