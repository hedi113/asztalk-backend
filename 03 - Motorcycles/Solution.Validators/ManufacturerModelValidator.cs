namespace Solution.Validators;

public class ManufacturerModelValidator : AbstractValidator<ManufacturerModel>
{
    public static string ManufacturerProperty => nameof(ManufacturerModel.Name);
    public static string GlobalProperty => "Global";



    public ManufacturerModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name of manufacturer is required!");
    }
}
