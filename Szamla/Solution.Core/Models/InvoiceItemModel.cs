namespace Solution.Core.Models;

public partial class InvoiceItemModel : ObservableObject
{
    [ObservableProperty]
    [JsonPropertyName("id")]
    private int id;

    [ObservableProperty]
    [JsonPropertyName("name")]
    private string name;

    [ObservableProperty]
    [JsonPropertyName("unitPrice")]
    private int unitPrice;

    [ObservableProperty]
    [JsonPropertyName("quantity")]
    private int quantity;

    public InvoiceItemModel() { }

    public InvoiceItemModel(InvoiceItemEntity entity) 
    {
        if(entity is null)
        {
            return;
        }

        this.id = entity.Id;
        this.name = entity.Name;
        this.unitPrice = entity.UnitPrice;
    }

    public InvoiceItemEntity ToEntity()
    {
        return new InvoiceItemEntity
        {
            Id = Id,
            Name = Name,
            UnitPrice = UnitPrice,
            Quantity = Quantity
        };
    }

    public override bool Equals(object? obj)
    {
        return obj is InvoiceItemModel model &&
            this.Id == model.Id &&
            this.Name == model.Name &&
            this.UnitPrice == model.UnitPrice &&
            this.Quantity == model.Quantity;
    }
}
