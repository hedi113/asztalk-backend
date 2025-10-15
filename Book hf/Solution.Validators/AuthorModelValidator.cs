namespace Solution.Validators;

public class AuthorModelValidator : BaseValidators<AuthorModel>
{
    public static string AuthorNameProperty => nameof(AuthorModel.Name);
    public static string AuthorBirthYearProperty => nameof(AuthorModel.BirthYear);
    public static string GlobalProperty => "Global";

    public AuthorModelValidator(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        if (IsPutMethod)
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required!");
        }
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name of author is required!");
        RuleFor(x => x.BirthYear)
            .NotNull()
                .WithMessage("Birthyear of author is required!")
            .GreaterThan(0)
                .WithMessage("Author's birthyear must be greater than 0!")
            .LessThanOrEqualTo(DateTime.Now.Year)
                .WithMessage("Author's birthyear can't be greater than the current year!");

    }
}
