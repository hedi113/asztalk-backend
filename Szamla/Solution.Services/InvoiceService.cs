using Solution.Database.Entities;

namespace Solution.Services;


public class InvoiceService(AppDbContext dbContext, IInvoiceItemService invoiceItemService) : IInvoiceService
{
    private const int ROW_COUNT = 20;

    public async Task<ErrorOr<InvoiceModel>> CreateAsync(InvoiceModel model)
    {
        bool exists = await dbContext.Invoices.AnyAsync(x => x.InvoiceNumber == model.InvoiceNumber);

        if (exists)
        {
            return Error.Conflict(description: "A számla már létezik!");
        }

        var invoice = new InvoiceEntity()
        {
            InvoiceNumber = model.InvoiceNumber,
            CreationDate = model.CreationDate,
            SumOfInvoiceItemValues = model.InvoiceItems.Sum(x => x.Sum!.Value),
            InvoiceItems = [.. model.InvoiceItems.Select(x => x.ToEntity())]
        };

        await dbContext.Invoices.AddAsync(invoice);
        await dbContext.SaveChangesAsync();

        return new InvoiceModel()
        {
            InvoiceNumber = model.InvoiceNumber,
            CreationDate = model.CreationDate,
            SumOfInvoiceItemValues = model.InvoiceItems.Sum(x => x.Sum!.Value)
        };    
    }

    public async Task<ErrorOr<Success>> UpdateAsync(InvoiceModel model)
    {
        //find invoice wiht the ide form the model
        //update the invocie with the properties from the model
        //save changes

        var invoice = await dbContext.Invoices
        .Include(i => i.InvoiceItems)
        .FirstOrDefaultAsync(i => i.Id == model.Id);

        if (invoice == null)
            return Error.NotFound();

        int sumOfInvoiceItemValues = model.InvoiceItems.Sum(x => x.Sum ?? 0);

        invoice.InvoiceNumber = model.InvoiceNumber;
        invoice.CreationDate = model.CreationDate;
        invoice.SumOfInvoiceItemValues = sumOfInvoiceItemValues;

        invoice.InvoiceItems.Clear();

        foreach (var item in model.InvoiceItems)
        {
            invoice.InvoiceItems.Add(item.ToEntity());
        }

        await dbContext.SaveChangesAsync();
        return new ErrorOr<Success>() { };
    }

    public async Task<ErrorOr<Success>> DeleteAsync(int invoiceId)
    {
        var result = await dbContext.Invoices.AsNoTracking()
                                             .Where(x => x.Id == invoiceId)
                                             .ExecuteDeleteAsync();
        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<InvoiceModel>> GetByIdAsync(int invoiceId)
    {
        var invoice = await dbContext.Invoices.FirstOrDefaultAsync(x => x.Id == invoiceId);

        if (invoice == null)
        {
            return Error.NotFound(description: "Nincs ilyen számla.");
        }

        return new InvoiceModel(invoice);
    }

    public async Task<ErrorOr<List<InvoiceModel>>> GetAllAsync() =>
        await dbContext.Invoices.AsNoTracking()
                                .Select(x => new InvoiceModel(x))
                                .ToListAsync();
    public async Task<ErrorOr<PaginationModel<InvoiceModel>>> GetPagedAsync(int page = 0)
    {
        page = page < 0 ? 0 : page - 1;

        var invoices = await dbContext.Invoices.AsNoTracking()
                                               .Skip(page * ROW_COUNT)
                                               .Take(ROW_COUNT)
                                               .Select(x => new InvoiceModel(x))
                                               .ToListAsync();

        var paginationModel = new PaginationModel<InvoiceModel> 
        {
            Items = invoices,
            Count = await dbContext.Invoices.CountAsync()
        };

        return paginationModel;
    }
}
