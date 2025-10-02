namespace Solution.Validators;

public class ManufacturerModelValidator : BaseValidators<ManufacturerModel>
{
    public static string ManufacturerProperty => nameof(ManufacturerModel.Name);
    public static string GlobalProperty => "Global";

    public ManufacturerModelValidator(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        if (IsPutMethod)
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required!");
        }
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name of manufacturer is required!");
    }
}
