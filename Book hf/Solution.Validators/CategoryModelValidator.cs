namespace Solution.Validators;

public class CategoryModelValidator : BaseValidators<CategoryModel>
{
    public static string CategoryProperty => nameof(CategoryModel.Name);
    public static string GlobalProperty => "Global";


    public CategoryModelValidator(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        if (IsPutMethod)
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required!");
        }

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name of type is required!");
    }
}
