using CommunityToolkit.Mvvm.Input;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class AppShellViewModel
{
    public IAsyncRelayCommand ExitCommand => new AsyncRelayCommand(OnExitAsync);

    public IAsyncRelayCommand AddNewBookCommand => new AsyncRelayCommand(OnAddNewBookAsync);
    public IAsyncRelayCommand AddNewAuthorCommand => new AsyncRelayCommand(OnAddNewAuthorAsync);
    public IAsyncRelayCommand AddNewCategoryCommand => new AsyncRelayCommand(OnAddNewCategoryAsync);

    public IAsyncRelayCommand ListAllBooksCommand => new AsyncRelayCommand(OnListAllBooksAsync);
    public IAsyncRelayCommand ListAllAuthorsCommand => new AsyncRelayCommand(OnListAllAuthorsAsync);
    public IAsyncRelayCommand ListAllCategoriesCommand => new AsyncRelayCommand(OnListAllCategoriesAsync);


    private async Task OnExitAsync() => Application.Current.Quit();

    private async Task OnAddNewBookAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(AddBookView.Name);
    }

    private async Task OnAddNewAuthorAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(AddAuthorView.Name);
    }
    private async Task OnAddNewCategoryAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(AddCategoryView.Name);
    }

    private async Task OnListAllBooksAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(BookListView.Name);
    }
    private async Task OnListAllAuthorsAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(AuthorListView.Name);
    }
    private async Task OnListAllCategoriesAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(CategoryListView.Name);
    }
}
