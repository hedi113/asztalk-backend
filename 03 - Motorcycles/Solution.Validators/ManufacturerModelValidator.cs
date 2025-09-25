namespace Solution.Validators;

public class ManufacturerModelValidator : BaseValidators<ManufacturerModel>
{
    public static string ManufacturerProperty => nameof(ManufacturerModel.Name);
    public static string GlobalProperty => "Global";

    public IHttpContextAccessor HttpContextAccessor { get; }

    public ManufacturerModelValidator(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name of manufacturer is required!");
    }
}
