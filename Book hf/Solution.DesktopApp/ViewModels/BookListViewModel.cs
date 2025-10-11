namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class BookListViewModel(IBookService bookService)
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
    public IAsyncRelayCommand DeleteCommand => new AsyncRelayCommand<string>((id) => OnDeleteAsync(id));
    #endregion

    [ObservableProperty]
    private ObservableCollection<BookModel> books;

    private int page = 1;
    private bool isLoading = false;
    private bool hasNextPage = false;
    private int numberOfBooksInDB = 0;

    private async Task OnAppearingAsync()
    {
        PreviousPageCommand = new Command(async () => await OnPreviousPageAsync(), () => page > 1 && !isLoading);
        NextPageCommand = new Command(async () => await OnNextPageAsync(), () => !isLoading && hasNextPage);

        await LoadBooksAsync();
    }

    private async Task OnDisappearingAsync()
    { }

    private async Task OnPreviousPageAsync()
    {
        if (isLoading) return;

        page = page <= 1 ? 1 : --page;
        await LoadBooksAsync();
    }

    private async Task OnNextPageAsync()
    {
        if (isLoading) return;

        page++;
        await LoadBooksAsync();
    }

    private async Task LoadBooksAsync()
    {
        isLoading = true;

        var result = await bookService.GetPagedAsync(page);

        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Books not loaded!", "OK");
            return;
        }

        Books = new ObservableCollection<BookModel>(result.Value.Items);
        numberOfBooksInDB = result.Value.Count;

        hasNextPage = numberOfBooksInDB - (page * 10) > 0;
        isLoading = false;

        ((Command)PreviousPageCommand).ChangeCanExecute();
        ((Command)NextPageCommand).ChangeCanExecute();
    }

    private async Task OnDeleteAsync(string? id)
    { 
        var result = await bookService.DeleteAsync(id);

        var message = result.IsError ? result.FirstError.Description : "Book deleted.";
        var title = result.IsError ? "Error" : "Information";

        if (!result.IsError)
        {
            var book = books.SingleOrDefault(x => x.PublicId == id);
            books.Remove(book);

            if(books.Count == 0)
            {
                await LoadBooksAsync();
            }
        }

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }
}
