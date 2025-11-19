using CommunityToolkit.Mvvm.Input;
using Solution.DesktopApp.Views;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class AppShellViewModel
{
    public IAsyncRelayCommand ExitCommand => new AsyncRelayCommand(OnExitAsync);

    public IAsyncRelayCommand ToInvoiceCommand => new AsyncRelayCommand(OnToInvoiceCommandAsync);


    private async Task OnExitAsync() => Application.Current.Quit();

   

    private async Task OnToInvoiceCommandAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(InvoiceView.Name);
    }
}
