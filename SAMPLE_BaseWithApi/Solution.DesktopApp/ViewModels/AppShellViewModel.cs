using CommunityToolkit.Mvvm.Input;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class AppShellViewModel
{
    public IAsyncRelayCommand ExitCommand => new AsyncRelayCommand(OnExitAsync);

    //Example
    //public IAsyncRelayCommand AddNewBookCommand => new AsyncRelayCommand(OnAddNewBookAsync);


    private async Task OnExitAsync() => Application.Current.Quit();

    //Example
    //private async Task OnAddNewBookAsync()
    //{
    //    Shell.Current.ClearNavigationStack();
    //    await Shell.Current.GoToAsync(AddBookView.Name);
    //}
}
