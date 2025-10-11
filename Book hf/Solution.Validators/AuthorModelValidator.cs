namespace Solution.Validators;

public class AuthorModelValidator : BaseValidators<AuthorModel>
{
    public static string AuthorProperty => nameof(AuthorModel.Name);
    public static string GlobalProperty => "Global";

    public AuthorModelValidator(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        if (IsPutMethod)
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required!");
        }
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name of author is required!");
        RuleFor(x => x.BirthYear).NotEmpty().WithMessage("Birthyear of author is required!");
        RuleFor(x => x.BirthYear).GreaterThan(0).LessThan(DateTime.Now.Year-10);
    }
}
