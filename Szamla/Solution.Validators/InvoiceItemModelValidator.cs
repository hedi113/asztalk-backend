namespace Solution.Validators;

public class InvoiceItemModelValidator : BaseValidators<InvoiceItemModel>
{
    public static string InvoiceItemNameProperty => nameof(InvoiceItemModel.Name);
    public static string InvoiceItemUnitPriceProperty => nameof(InvoiceItemModel.UnitPrice);
    public static string InvoiceItemQuantityProperty => nameof(InvoiceItemModel.Quantity);
    public static string GlobalProperty => "Global";

    public InvoiceItemModelValidator(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("A name is required for the invoice!");
        RuleFor(x => x.UnitPrice)
            .NotNull()
                .WithMessage("The price value of the unit is required!")
            .GreaterThan(0)
                .WithMessage("The price value of the unit must be greater than 0!");
        RuleFor(x => x.Quantity)
            .NotNull()
                .WithMessage("A quantity must be given!")
            .GreaterThan(0)
                .WithMessage("The value of quantity must be greater than 1!");
    }
}
