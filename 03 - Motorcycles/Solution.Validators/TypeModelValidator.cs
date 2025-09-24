using Solution.Database.Entities;
using Solution.DataBase;

namespace Solution.Validators;

public class TypeModelValidator : AbstractValidator<TypeModel>
{
    public static string TypeProperty => nameof(TypeModel.Name);
    public static string GlobalProperty => "Global";

    AppDbContext _appDbContext;

    public TypeModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name of type is required!");
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) =>
        {
            MotorcycleTypeEntity? type = await _appDbContext.Types.FindAsync(new object?[] { id }, cancellationToken: cancellation);

            bool exists = false;

            if (type != null)
            {
                return exists;
            }
            else
            {
                return !exists;
            }
        }).WithMessage("ID Must be unique");
    }
}
