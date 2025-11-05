namespace Solution.Services;

public class InvoiceItemService(AppDbContext dbContext) : IInvoiceItemService
{
    private const int ROW_COUNT = 10;

    public async Task<ErrorOr<InvoiceItemModel>> CreateAsync(InvoiceItemModel model)
    {
        bool exists = await dbContext.InvoiceItems.AnyAsync(x => x.Name == model.Name && x.UnitPrice == model.UnitPrice && x.Quantity == model.Quantity);

        if(exists)
        {
            return Error.Conflict(description: "Invoice item already exists!");
        }

        var invoiceItem = model.ToEntity();

        await dbContext.InvoiceItems.AddAsync(invoiceItem);
        await dbContext.SaveChangesAsync();

        return new InvoiceItemModel(invoiceItem)
        {
            Name = model.Name,
            UnitPrice = model.UnitPrice,
            Quantity = model.Quantity
        };
    }


    public async Task<ErrorOr<Success>> UpdateAsync(InvoiceItemModel model)
    {
        var result = await dbContext.InvoiceItems.AsNoTracking()
                                                 .Where(x => x.Id == model.Id)
                                                 .ExecuteUpdateAsync(x => x.SetProperty());
    }
}

