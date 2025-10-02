namespace Solution.Validators;

public class TypeModelValidator : BaseValidators<TypeModel>
{
    public static string TypeProperty => nameof(TypeModel.Name);
    public static string GlobalProperty => "Global";


    public TypeModelValidator(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        if (IsPutMethod)
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required!");
        }

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name of type is required!");
    }
}
