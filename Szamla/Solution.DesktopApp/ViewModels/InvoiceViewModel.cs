namespace Solution.DesktopApp.ViewModels;

public partial class InvoiceViewModel : InvoiceModel
{
    [ObservableProperty]
    private InvoiceItemModel invoiceItem;

    public InvoiceViewModel()
    {
        this.InvoiceItems = new List<InvoiceItemModel>();
    }
}
