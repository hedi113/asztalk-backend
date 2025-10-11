namespace Solution.DesktopApp.Views;

public partial class AddAuthorView : ContentPage
{
	public AddAuthorViewModel ViewModel => this.BindingContext as AddAuthorViewModel;

	public static string Name => nameof(AddAuthorView);

    public AddAuthorView(AddAuthorViewModel viewModel)
	{
		this.BindingContext = viewModel;

		InitializeComponent();
	}
}