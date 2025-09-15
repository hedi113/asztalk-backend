namespace Solution.Validators;

public class TypeModelValidator : AbstractValidator<TypeModel>
{
    public static string TypeProperty => nameof(TypeModel.Name);
    public static string GlobalProperty => "Global";



    public TypeModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name of type is required!");
    }
}
