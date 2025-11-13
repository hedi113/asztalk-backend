namespace Solution.Core.Models;

public partial class InvoiceModel : ObservableObject
{
    [ObservableProperty]
    [JsonPropertyName("id")]
    private int id;

    [ObservableProperty]
    [JsonPropertyName("invoiceNumber")]
    private string invoiceNumber;

    [ObservableProperty]
    [JsonPropertyName("creationDate")]
    private DateTime creationDate;

    [ObservableProperty]
    [JsonPropertyName("sumOfInvoiceItemValues")]
    private int sumOfInvoiceItemValues;

    [ObservableProperty]
    [JsonPropertyName("")]
    private ICollection<InvoiceItemModel> invoiceItems;

    public InvoiceModel() { }

    public InvoiceModel(InvoiceEntity entity)
    {
        if(entity is null)
        {
            return;
        }

        this.id = entity.Id;
        this.invoiceNumber = entity.InvoiceNumber;
        this.creationDate = entity.CreationDate;
        this.sumOfInvoiceItemValues = entity.SumOfInvoiceItemValues;
        this.invoiceItems = entity.InvoiceItems.Select(x => new InvoiceItemModel(x)).ToList();
    }

    public InvoiceEntity ToEntity()
    {
        return new InvoiceEntity
        {
            Id = Id,
            InvoiceNumber = InvoiceNumber,
            CreationDate = CreationDate,
            SumOfInvoiceItemValues = SumOfInvoiceItemValues,
            InvoiceItems = InvoiceItems.Select(x => x.ToEntity()).ToList(),
            
        };
    }

    public override bool Equals(object? obj)
    {
        return obj is InvoiceModel model &&
            this.Id == model.Id &&
            this.InvoiceNumber == model.InvoiceNumber &&
            this.CreationDate == model.CreationDate &&
            this.SumOfInvoiceItemValues == model.SumOfInvoiceItemValues;
    }
}
