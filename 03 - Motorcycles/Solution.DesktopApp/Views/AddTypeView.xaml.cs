namespace Solution.DesktopApp.Views;

public partial class AddTypeView : ContentPage
{
	public AddTypeViewModel ViewModel => this.BindingContext as AddTypeViewModel;

	public static string Name => nameof(AddTypeView);

    public AddTypeView(AddTypeViewModel viewModel)
	{
		this.BindingContext = viewModel;

		InitializeComponent();
	}
}