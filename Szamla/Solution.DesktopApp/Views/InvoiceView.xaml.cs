namespace Solution.DesktopApp.Views;

public partial class InvoiceView : ContentPage
{
	public InvoiceViewModel ViewModel => this.BindingContext as InvoiceViewModel;

	public static string Name => nameof(InvoiceView);

    public InvoiceView(InvoiceViewModel viewModel)
	{
		this.BindingContext = viewModel;

		InitializeComponent();
	}
}