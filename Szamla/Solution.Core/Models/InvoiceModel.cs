using System.Collections.ObjectModel;

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
    private DateTime creationDate = DateTime.Now;

    [ObservableProperty]
    [JsonPropertyName("sumOfInvoiceItemValues")]
    private int sumOfInvoiceItemValues;

    [JsonPropertyName("invoiceItems")]
    [ObservableProperty]
    public ObservableCollection<InvoiceItemModel> invoiceItems = new ObservableCollection<InvoiceItemModel>();

    public InvoiceModel() { }

    public InvoiceModel(InvoiceEntity entity)
    {
        if(entity is null)
        {
            return;
        }

        var invoiceItems = entity.InvoiceItems.Select(x => new InvoiceItemModel(x)).ToList();

        Id = entity.Id;
        InvoiceNumber = entity.InvoiceNumber;
        CreationDate = entity.CreationDate;
        SumOfInvoiceItemValues = entity.SumOfInvoiceItemValues;
        InvoiceItems = new ObservableCollection<InvoiceItemModel>(invoiceItems);
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
