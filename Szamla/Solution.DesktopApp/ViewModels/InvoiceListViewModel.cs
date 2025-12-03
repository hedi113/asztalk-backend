namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class InvoiceListViewModel(AppDbContext dbContext, IInvoiceService invoiceService)
{
    #region life cycle commands
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
    #endregion



    #region component commands
    public IAsyncRelayCommand DeleteCommand => new AsyncRelayCommand<int>((id) => OnDeleteAsync(id));
    public IAsyncRelayCommand UpdateCommand => new AsyncRelayCommand<int>((id) => OnUpdateAsync(id));
    #endregion

    [ObservableProperty]
    private ObservableCollection<InvoiceModel> invoices;

    [ObservableProperty]
    private IList<InvoiceItemModel> invoiceItems = [];

    private int page = 1;
    private bool isLoading = false;
    private bool hasNextPage = false;
    private int numberOfInvoicesInDB = 0;

    private async Task OnAppearingAsync()
    {
        await LoadInvoicesAsync();
    }

    private async Task OnDisappearingAsync()
    { }



    private async Task LoadInvoicesAsync()
    {
        isLoading = true;

        var result = await invoiceService.GetPagedAsync(page);

        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Invoices not loaded!", "OK");
            return;
        }

        Invoices = new ObservableCollection<InvoiceModel>(result.Value.Items);
        numberOfInvoicesInDB = result.Value.Count;

        hasNextPage = numberOfInvoicesInDB - (page * 20) > 0;
        isLoading = false;
    }


    private async Task OnDeleteAsync(int id)
    {
        var result = await invoiceService.DeleteAsync(id);

        var message = result.IsError ? result.FirstError.Description : "Invoice deleted.";
        var title = result.IsError ? "Error" : "Information";

        if(result.IsError)
        {
            var invoice = invoices.SingleOrDefault(x => x.Id == id);
            invoices.Remove(invoice);

            if(invoices.Count == 0)
            {
                await LoadInvoicesAsync();
            }
        }

        LoadInvoicesAsync();

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private async Task OnUpdateAsync(int id)
    {
        await Task.Run(LoadInvoiceItemsAsync);

        var invoice = dbContext.Invoices.Where(x => x.Id == id).FirstOrDefault();

        InvoiceModel model = new();

        model.Id = id;
        model.InvoiceNumber = invoice.InvoiceNumber;
        model.CreationDate = invoice.CreationDate;
        model.SumOfInvoiceItemValues = invoice.SumOfInvoiceItemValues;
        model.InvoiceItems = [.. invoice.InvoiceItems.Select(x => new InvoiceItemModel(x))];

        var result = await invoiceService.UpdateAsync(new InvoiceModel(invoice));
        var message = result.IsError ? result.FirstError.Description : "Invoice saved";
        var title = result.IsError ? "Error" : "Information";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private async Task LoadInvoiceItemsAsync()
    {
        InvoiceItems = await dbContext.InvoiceItems.AsNoTracking()
                                       .Select(x => new InvoiceItemModel(x))
                                       .ToListAsync();
    }
}
