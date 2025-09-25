namespace Solution.Validators;

public class TypeModelValidator : BaseValidators<TypeModel>
{
    public static string TypeProperty => nameof(TypeModel.Name);
    public static string GlobalProperty => "Global";

    public IHttpContextAccessor HttpContextAccessor { get; }


    public TypeModelValidator(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name of type is required!");

    }
}
