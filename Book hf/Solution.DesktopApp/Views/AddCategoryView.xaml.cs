namespace Solution.DesktopApp.Views;

public partial class AddCategoryView : ContentPage
{
	public AddCategoryViewModel ViewModel => this.BindingContext as AddCategoryViewModel;

	public static string Name => nameof(AddCategoryView);

    public AddCategoryView(AddCategoryViewModel viewModel)
	{
		this.BindingContext = viewModel;

		InitializeComponent();
	}
}