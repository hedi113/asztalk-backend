using CommunityToolkit.Mvvm.ComponentModel;

namespace Solution.Core.Models;

public partial class BookModel : ObservableObject
{
    [ObservableProperty]
    [JsonPropertyName("id")]
    public string publicId;

    [ObservableProperty]
    [JsonPropertyName("imageId")]
    public string imageId;

    [ObservableProperty]
    [JsonPropertyName("webContentLink")]
    public string webContentLink;

    [ObservableProperty]
    [JsonPropertyName("author")]
    public AuthorModel author;

    [ObservableProperty]
    [JsonPropertyName("category")]
    public CategoryModel category;

    [ObservableProperty]
    [JsonPropertyName("title")]
    public string title;

    [ObservableProperty]
    [JsonPropertyName("publisher")]
    public string publisher;

    [ObservableProperty]
    [JsonPropertyName("pageNumber")]
    public int? pageNumber;

    [ObservableProperty]
    [JsonPropertyName("releaseDate")]
    public DateTime releaseDate;

    public BookModel() { }

    public BookModel(BookEntity entity)
    {
        this.publicId = entity.PublicId;
        this.imageId = entity.ImageId;
        this.webContentLink = entity.WebContentLink;
        this.author = new AuthorModel(entity.Author);
        this.category = new CategoryModel(entity.Category);
        this.title = entity.Title;
        this.publisher = entity.Publisher;
        this.pageNumber = (int)entity.PageNumber;
        this.releaseDate= entity.ReleaseDate;
    }

    public BookEntity ToEntity()
    {
        return new BookEntity
        {
            PublicId = PublicId,
            AuthorId = Author.Id,
            CategoryId = Category.Id,
            ImageId = ImageId,
            WebContentLink = WebContentLink,
            Title = Title,
            Publisher = Publisher,
            PageNumber = PageNumber.Value,
            ReleaseDate = ReleaseDate
        };
    }

    public void ToEntity(BookEntity entity)
    {
        entity.PublicId = PublicId;
        entity.AuthorId = Author.Id;
        entity.CategoryId = Category.Id;
        entity.ImageId = ImageId;
        entity.WebContentLink = WebContentLink;
        entity.Title = Title;
        entity.Publisher = Publisher;
        entity.PageNumber = PageNumber.Value;
        entity.ReleaseDate = ReleaseDate;
    }
}
