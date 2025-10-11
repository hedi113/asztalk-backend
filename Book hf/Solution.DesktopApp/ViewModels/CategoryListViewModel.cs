namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class CategoryListViewModel(ICategoryService categoryService)
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
    private ObservableCollection<CategoryModel> categories;

    private int page = 1;
    private bool isLoading = false;
    private bool hasNextPage = false;
    private int numberOfCategoriesInDB = 0;

    private async Task OnAppearingAsync()
    {
        PreviousPageCommand = new Command(async () => await OnPreviousPageAsync(), () => page > 1 && !isLoading);
        NextPageCommand = new Command(async () => await OnNextPageAsync(), () => !isLoading && hasNextPage);

        await LoadCategoriesAsync();
    }

    private async Task OnDisappearingAsync()
    { }

    private async Task OnPreviousPageAsync()
    {
        if (isLoading) return;

        page = page <= 1 ? 1 : --page;
        await LoadCategoriesAsync();
    }

    private async Task OnNextPageAsync()
    {
        if (isLoading) return;

        page++;
        await LoadCategoriesAsync();
    }

    private async Task LoadCategoriesAsync()
    {
        isLoading = true;

        var result = await categoryService.GetPagedAsync(page);

        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Categories not loaded!", "OK");
            return;
        }

        Categories = new ObservableCollection<CategoryModel>(result.Value.Items);
        numberOfCategoriesInDB = result.Value.Count;

        hasNextPage = numberOfCategoriesInDB - (page * 10) > 0;
        isLoading = false;

        ((Command)PreviousPageCommand).ChangeCanExecute();
        ((Command)NextPageCommand).ChangeCanExecute();
    }

    private async Task OnDeleteAsync(int id)
    { 
        var result = await categoryService.DeleteAsync(id);

        var message = result.IsError ? result.FirstError.Description : "Category deleted.";
        var title = result.IsError ? "Error" : "Information";

        if (!result.IsError)
        {
            var category = categories.SingleOrDefault(x => x.Id == id);
            categories.Remove(category);

            if(categories.Count == 0)
            {
                await LoadCategoriesAsync();
            }
        }

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }
}
