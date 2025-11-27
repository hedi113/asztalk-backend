using CommunityToolkit.Mvvm.Input;
using Solution.DesktopApp.Views;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class AppShellViewModel
{
    public IAsyncRelayCommand ExitCommand => new AsyncRelayCommand(OnExitAsync);

    public IAsyncRelayCommand ToInvoiceCommand => new AsyncRelayCommand(OnToInvoiceCommandAsync);

    public IAsyncRelayCommand ToInvoiceListCommand => new AsyncRelayCommand(OnToInvoiceListCommandAsync);

    private async Task OnExitAsync() => Application.Current.Quit();

   

    private async Task OnToInvoiceCommandAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(InvoiceView.Name);
    }

    private async Task OnToInvoiceListCommandAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(InvoiceListView.Name);
    }
}
