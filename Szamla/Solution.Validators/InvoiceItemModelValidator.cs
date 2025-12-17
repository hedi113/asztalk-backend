namespace Solution.Validators;

public class InvoiceItemModelValidator : BaseValidators<InvoiceItemModel>
{
    public static string InvoiceItemNameProperty => nameof(InvoiceItemModel.Name);
    public static string InvoiceItemUnitPriceProperty => nameof(InvoiceItemModel.UnitPrice);
    public static string InvoiceItemQuantityProperty => nameof(InvoiceItemModel.Quantity);
    public static string GlobalProperty => "Global";

    public InvoiceItemModelValidator(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Név megadása kötelező!");
        RuleFor(x => x.UnitPrice)
            .NotNull()
                .WithMessage("Az ár megadása kötelező!")
            .GreaterThan(0)
                .WithMessage("Az ár-értéknek 0-nál nagyobbnak kell lennie!");
        RuleFor(x => x.Quantity)
            .NotNull()
                .WithMessage("A mennyiség megadása kötelező!")
            .GreaterThan(0)
                .WithMessage("A mennyiség nem lehet 0!");
    }
}
