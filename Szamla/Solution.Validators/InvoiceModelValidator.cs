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
                .WithMessage("Invoice number is required!")
            .MaximumLength(24)
                .WithMessage("Invoice number can't be longer than 24 characters!")
            .MinimumLength(24)
                .WithMessage("At least 24 characters is required for the invoice number!");
        RuleFor(x => x.CreationDate)
            .NotEmpty()
                .WithMessage("A date must be given!")
            .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("The creation date can't be in the future!");
        RuleFor(x => x.SumOfInvoiceItemValues)
            .GreaterThan(0)
                .WithMessage("The sum of the values must be greater than 0!");
        RuleFor(x => x.InvoiceItems)
            .NotEmpty()
                .WithMessage("Add at least 1 invoice item to the invoice!");
    }

}
