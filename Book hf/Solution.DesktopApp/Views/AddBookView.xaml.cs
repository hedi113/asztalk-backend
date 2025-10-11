namespace Solution.DesktopApp.Views;

public partial class AddBookView : ContentPage
{
	public AddBookViewModel ViewModel => this.BindingContext as AddBookViewModel;

	public static string Name => nameof(AddBookView);

    public AddBookView(AddBookViewModel viewModel)
	{
		this.BindingContext = viewModel;

		InitializeComponent();
	}
}