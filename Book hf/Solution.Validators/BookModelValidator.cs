namespace Solution.Validators;

public class BookModelValidator : BaseValidators<BookModel>
{
    public static string TitleProperty => nameof(BookModel.Title);
    public static string PageNumberProperty => nameof(BookModel.PageNumber);
    public static string PublisherProperty => nameof(BookModel.Publisher);
    public static string ReleaseDateProperty => nameof(BookModel.ReleaseDate);
    public static string AuthorProperty => nameof(BookModel.Author);
    public static string CategoryProperty => nameof(BookModel.Category);
    public static string GlobalProperty => "Global";



    public BookModelValidator(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {

        if (IsPutMethod)
        {
            RuleFor(x => x.PublicId).NotEmpty().WithMessage("Id is required!");
        }

        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required!");
        RuleFor(x => x.PageNumber).GreaterThan(0).NotNull().WithMessage("The number of pages has to be greater than 0!");
        RuleFor(x => x.Publisher).NotNull().WithMessage("Publisher is required!");
        RuleFor(x => x.ReleaseDate).NotNull().WithMessage("A release date must be selected!");
        RuleFor(x => x.Author).NotNull().WithMessage("An author must be selected!");
        RuleFor(x => x.Category).NotNull().WithMessage("Category is required!");
    }
}
