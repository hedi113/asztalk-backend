namespace Solution.Validators;

public class MotorcycleModelValidator : BaseValidators<MotorcycleModel>
{
    public static string ModelProperty => nameof(MotorcycleModel.Model);
    public static string CubicProperty => nameof(MotorcycleModel.Cubic);
    public static string TypeProperty => nameof(MotorcycleModel.Type);
    public static string ManufacturerProperty => nameof(MotorcycleModel.Manufacturer);
    public static string NumberOfCylindersProperty => nameof(MotorcycleModel.NumberOfCylinders);
    public static string ReleaseYearProperty => nameof(MotorcycleModel.ReleaseYear);
    public static string GlobalProperty => "Global";



    public MotorcycleModelValidator(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {

        if (IsPutMethod)
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required!");
        }

        RuleFor(x => x.Model).NotEmpty().WithMessage("Model is required!");
        RuleFor(x => x.Cubic).GreaterThan(0).NotNull().WithMessage("Cubic has to be greater than 0!");
        RuleFor(x => x.Manufacturer).NotNull().WithMessage("Manufacturer is required!");
        RuleFor(x => x.NumberOfCylinders).GreaterThan(0).NotNull().WithMessage("Number of cylinders has to be greater than 0!");
        RuleFor(x => x.ReleaseYear).InclusiveBetween(1885, DateTime.Now.Year).NotNull().WithMessage("Invalid release year!");
        RuleFor(x => x.Type).NotNull().WithMessage("Type is required!");
        RuleFor(x => x.Manufacturer.Id).GreaterThan(0).NotNull().WithMessage("Manufacturer id has to be greater than 0!");
        RuleFor(x => x.Type.Id).GreaterThan(0).NotNull().WithMessage("Type id has to be greater than 0!");
    }
}
