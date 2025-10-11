using Solution.Core.Interfaces;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class AuthorListViewModel(IAuthorService authorService)
{
    #region life cycle commands
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
    #endregion

    #region paging commands
    public ICommand PreviousPageCommand { get; private set; }
    public ICommand NextPageCommand { get; private set; }
    #endregion

    #region component commands
    public IAsyncRelayCommand DeleteCommand => new AsyncRelayCommand<int>((id) => OnDeleteAsync(id));
    #endregion

    [ObservableProperty]
    private ObservableCollection<AuthorModel> authors;

    private int page = 1;
    private bool isLoading = false;
    private bool hasNextPage = false;
    private int numberOfAuthorsInDB = 0;

    private async Task OnAppearingAsync()
    {
        PreviousPageCommand = new Command(async () => await OnPreviousPageAsync(), () => page > 1 && !isLoading);
        NextPageCommand = new Command(async () => await OnNextPageAsync(), () => !isLoading && hasNextPage);

        await LoadAuthorsAsync();
    }

    private async Task OnDisappearingAsync()
    { }

    private async Task OnPreviousPageAsync()
    {
        if (isLoading) return;

        page = page <= 1 ? 1 : --page;
        await LoadAuthorsAsync();
    }

    private async Task OnNextPageAsync()
    {
        if (isLoading) return;

        page++;
        await LoadAuthorsAsync();
    }

    private async Task LoadAuthorsAsync()
    {
        isLoading = true;

        var result = await authorService.GetPagedAsync(page);

        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Authors not loaded!", "OK");
            return;
        }

        Authors = new ObservableCollection<AuthorModel>(result.Value.Items);
        numberOfAuthorsInDB = result.Value.Count;

        hasNextPage = numberOfAuthorsInDB - (page * 10) > 0;
        isLoading = false;

        ((Command)PreviousPageCommand).ChangeCanExecute();
        ((Command)NextPageCommand).ChangeCanExecute();
    }

    private async Task OnDeleteAsync(int id)
    { 
        var result = await authorService.DeleteAsync(id);

        var message = result.IsError ? result.FirstError.Description : "Author deleted.";
        var title = result.IsError ? "Error" : "Information";

        if (!result.IsError)
        {
            var author = authors.SingleOrDefault(x => x.Id == id);
            authors.Remove(author);

            if(authors.Count == 0)
            {
                await LoadAuthorsAsync();
            }
        }

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }
}
