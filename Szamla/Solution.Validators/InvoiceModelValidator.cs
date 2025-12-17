namespace Solution.Validators;

public class InvoiceModelValidator : BaseValidators<InvoiceModel>
{
    public static string InvoiceNumberProperty => nameof(InvoiceModel.InvoiceNumber);
    public static string InvoiceCreationDateProperty => nameof(InvoiceModel.CreationDate);
    public static string SumOfInvoiceItemValuesProperty => nameof(InvoiceModel.SumOfInvoiceItemValues);
    public static string InvoiceItemsProperty => nameof(InvoiceModel.InvoiceItems);
    public static string GlobalProperty => "Global";

    public InvoiceModelValidator(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        RuleFor(x => x.InvoiceNumber)
            .NotEmpty()
                .WithMessage("A számlaszám megadása kötelező!")
            .MaximumLength(24)
                .WithMessage("A számlaszám nem állhat több mint 24 karakterből!")
            .MinimumLength(24)
                .WithMessage("A számlaszámnak minimum 24 karakterből kell állnia!");
        RuleFor(x => x.CreationDate)
            .NotEmpty()
                .WithMessage("A dátum megadása kötelező!");
    }

}
