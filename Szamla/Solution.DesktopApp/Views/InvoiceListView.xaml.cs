namespace Solution.DesktopApp.Views;

public partial class InvoiceListView : ContentPage
{
	public InvoiceListViewModel ViewModel => this.BindingContext as InvoiceListViewModel;

	public static string Name => nameof(InvoiceListView);

    public InvoiceListView(InvoiceListViewModel viewModel)
	{
		this.BindingContext = viewModel;

		InitializeComponent();
	}
}