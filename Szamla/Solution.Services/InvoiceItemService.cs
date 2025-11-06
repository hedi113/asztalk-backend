namespace Solution.Services;

public class InvoiceItemService(AppDbContext dbContext) : IInvoiceItemService
{
    private const int ROW_COUNT = 20;

    public async Task<ErrorOr<InvoiceItemModel>> CreateAsync(InvoiceItemModel model, int invoiceId)
    {
        bool exists = await dbContext.InvoiceItems.AnyAsync(x => x.Name == model.Name && x.UnitPrice == model.UnitPrice && x.Quantity == model.Quantity && x.InvoiceId == model.InvoiceId);

        if (exists)
        {
            return Error.Conflict(description: "Invoice item already exists!");
        }

        var invoiceItem = model.ToEntity(invoiceId);

        await dbContext.InvoiceItems.AddAsync(invoiceItem);
        await dbContext.SaveChangesAsync();

        return new InvoiceItemModel(invoiceItem)
        {
            Name = model.Name,
            UnitPrice = model.UnitPrice,
            Quantity = model.Quantity,
            InvoiceId = invoiceId
        };
    }


    public async Task<ErrorOr<Success>> UpdateAsync(InvoiceItemModel model)
    {
        var result = await dbContext.InvoiceItems.AsNoTracking()
                                                 .Where(x => x.Id == model.Id)
                                                 .ExecuteUpdateAsync(x => x.SetProperty(p => p.Name, model.Name).SetProperty(p => p.UnitPrice, model.UnitPrice).SetProperty(p => p.Quantity, model.Quantity).SetProperty(p => p.InvoiceId, model.InvoiceId));
        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<Success>> DeleteAsync(int invoiceItemId)
    {
        var result = await dbContext.InvoiceItems.AsNoTracking()
                                                 .Where(x => x.Id == invoiceItemId)
                                                 .ExecuteDeleteAsync();
        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<InvoiceItemModel>> GetByIdAsync(int invoiceItemId)
    {
        var invoiceItem = await dbContext.InvoiceItems.FirstOrDefaultAsync(x => x.Id == invoiceItemId);

        if (invoiceItem is null)
        {
            return Error.NotFound(description: "Invoice item not found.");
        }
        return new InvoiceItemModel(invoiceItem);
    }

    public async Task<ErrorOr<List<InvoiceItemModel>>> GetAllAsync() =>
        await dbContext.InvoiceItems.AsNoTracking().Select(x => new InvoiceItemModel(x)).ToListAsync();

    public async Task<ErrorOr<PaginationModel<InvoiceItemModel>>> GetPagedAsync(int page = 0)
    {
        page = page < 0 ? 0 : page - 1;

        var invoiceItems = await dbContext.InvoiceItems.AsNoTracking()
                                                       .Skip(page * ROW_COUNT)
                                                       .Take(ROW_COUNT)
                                                       .Select(x => new InvoiceItemModel(x))
                                                       .ToListAsync();
        var paginationModel = new PaginationModel<InvoiceItemModel>
        {
            Items = invoiceItems,
            Count = await dbContext.InvoiceItems.CountAsync()
        };
        return paginationModel;
    }
}
