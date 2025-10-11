using Solution.Core.Interfaces;

namespace Solution.DesktopApp.ViewModels;

public partial class AddBookViewModel(
    AppDbContext dbContext,
    IBookService bookService,
    IGoogleDriveService googleDriveService) : BookModel, IQueryAttributable
{
    #region life cycle commands
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingkAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
    #endregion

    #region validation
    public IRelayCommand ValidateCommand => new AsyncRelayCommand<string>(OnValidateAsync);
    #endregion

    #region event commands
    public IAsyncRelayCommand SubmitCommand => new AsyncRelayCommand(OnSubmitAsync);
    public IAsyncRelayCommand ImageSelectCommand => new AsyncRelayCommand(OnImageSelectAsync);
    #endregion

    private BookModelValidator validator => new BookModelValidator(null);

    [ObservableProperty]
    private ValidationResult validationResult = new ValidationResult();

    private delegate Task ButtonActionDelagate();
    private ButtonActionDelagate asyncButtonAction;

    [ObservableProperty]
    private string title;

    [ObservableProperty]
    private IList<AuthorModel> authors = [];

    [ObservableProperty]
    private IList<CategoryModel> categories = [];

    [ObservableProperty]
    private ImageSource image;

    private FileResult selectedFile = null;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        await Task.Run(LoadAuthorsAsync);
        await Task.Run(LoadCategoriesAsync);

        bool hasValue = query.TryGetValue("Book", out object result);

        if(!hasValue)
        {
            asyncButtonAction = OnSaveAsync;
            Title = "Add new book";
            return;
        }

        BookModel book = result as BookModel;

        this.PublicId = book.PublicId;
        this.PageNumber = book.PageNumber;
        this.Publisher = book.Publisher;
        this.ReleaseDate = book.ReleaseDate;
        this.Author = book.Author;
        this.Category = book.Category;
        this.ImageId = book.ImageId;
        this.WebContentLink = book.WebContentLink;

        if(!string.IsNullOrEmpty(book.WebContentLink))
        {
            Image = new UriImageSource
            {
                Uri = new Uri(book.WebContentLink),
                CacheValidity = new TimeSpan(10, 0, 0, 0)
            };
        }

        asyncButtonAction = OnUpdateAsync;
        Title = "Update book";
    }

    private async Task OnAppearingkAsync()
    {
    }

    private async Task OnDisappearingAsync()
    { }

    private async Task OnSubmitAsync() => await asyncButtonAction();

    private async Task OnSaveAsync()
    {
        this.ValidationResult = await validator.ValidateAsync(this);

        if(!ValidationResult.IsValid)
        {
            return;
        }

        await UploaImageAsync();

        var result = await bookService.CreateAsync(this);
        var message = result.IsError ? result.FirstError.Description : "Book saved.";
        var title = result.IsError ? "Error" : "Information";

        if (!result.IsError)
        {
            ClearForm();
        }

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private async Task OnUpdateAsync()
    {
        this.ValidationResult = await validator.ValidateAsync(this);

        if (!ValidationResult.IsValid)
        {
            return;
        }

        await UploaImageAsync();

        var result = await bookService.UpdateAsync(this);

        var message = result.IsError ? result.FirstError.Description : "Book updated.";
        var title = result.IsError ? "Error" : "Information";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private async Task OnImageSelectAsync()
    {
        selectedFile = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Images,
            PickerTitle = "Please select an image for the book cover"
        });

        if(selectedFile is null)
        {
            return;
        }

        var stream = await selectedFile.OpenReadAsync();
        Image = ImageSource.FromStream(() => stream);
    }

    private async Task UploaImageAsync()
    {
        if (selectedFile is null)
        {
            return;
        }

        var imageUploadResult = await googleDriveService.UploadFileAsync(selectedFile);

        var message = imageUploadResult.IsError ? imageUploadResult.FirstError.Description : "The image selected for the book's cover has been uploaded.";
        var title = imageUploadResult.IsError ? "Error" : "Information";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");

        this.ImageId = imageUploadResult.IsError ? null : imageUploadResult.Value.Id;
        this.WebContentLink = imageUploadResult.IsError ? null : imageUploadResult.Value.WebContentLink;
    }

    private async Task LoadAuthorsAsync()
    {
        Authors = await dbContext.Authors.AsNoTracking()
                                                     .OrderBy(x => x.Name)
                                                     .Select(x => new AuthorModel(x))
                                                     .ToListAsync();
    }

    private async Task LoadCategoriesAsync()
    {
        Categories = await dbContext.Categories.AsNoTracking()
                                     .OrderBy(x => x.Name)
                                     .Select(x => new CategoryModel(x))
                                     .ToListAsync();
    }

    private void ClearForm()
    {
        this.Title = null;
        this.PageNumber = 0;
        this.Publisher = null;
        this.ReleaseDate = DateTime.Today;
        this.Author = null;
        this.Category = null;

        this.Image = null;
        this.selectedFile = null;
        this.WebContentLink = null;
        this.ImageId = null;
    }

    private async Task OnValidateAsync(string propertyName)
    {
        var result = await validator.ValidateAsync(this, options => options.IncludeProperties(propertyName));

        ValidationResult.Errors.Remove(ValidationResult.Errors.FirstOrDefault(x => x.PropertyName == propertyName));
        
        ValidationResult.Errors.Remove(ValidationResult.Errors.FirstOrDefault(x => x.PropertyName == BookModelValidator.GlobalProperty));
        ValidationResult.Errors.AddRange(result.Errors);

        OnPropertyChanged(nameof(ValidationResult));
    }
}
