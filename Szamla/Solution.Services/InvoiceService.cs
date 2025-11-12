using Solution.Core.Interfaces;
using Solution.Database.Entities;

namespace Solution.Services;


public class InvoiceService(AppDbContext dbContext, IInvoiceItemService invoiceItemService) : IInvoiceService
{
    private const int ROW_COUNT = 20;

    public async Task<ErrorOr<InvoiceModel>> CreateAsync(InvoiceModel model)
    {
        ErrorOr<List<InvoiceItemModel>> invoiceItems = invoiceItemService.GetAllAsync().Result;
        int sumOfInvoiceItemValues = invoiceItems.Value.Where(x => x.InvoiceId == model.Id).Sum(x => x.UnitPrice);

        bool exists = await dbContext.Invoices.AnyAsync(x => x.InvoiceNumber == model.InvoiceNumber);

        if (exists)
        {
            return Error.Conflict(description: "Invoice already exists!");
        }

            var invoice = new InvoiceEntity()
            {
                InvoiceNumber = model.InvoiceNumber,
                CreationDate = model.CreationDate,
                SumOfInvoiceItemValues = sumOfInvoiceItemValues,
                InvoiceItems = model.InvoiceItems
            };

            await dbContext.Invoices.AddAsync(invoice);
            await dbContext.SaveChangesAsync();

            return new InvoiceModel()
            {
                InvoiceNumber = model.InvoiceNumber,
                CreationDate = model.CreationDate,
                SumOfInvoiceItemValues = sumOfInvoiceItemValues
            };
        

            
    }

    public async Task<ErrorOr<Success>> UpdateAsync(InvoiceModel model)
    {
        ErrorOr<List<InvoiceItemModel>> invoiceItems = await invoiceItemService.GetAllAsync();
        int sumOfInvoiceItemValues = invoiceItems.Value.Where(x => x.InvoiceId == model.Id).Sum(x => x.UnitPrice);

        var result = new InvoiceEntity()
        {
            InvoiceNumber = model.InvoiceNumber,
            CreationDate = model.CreationDate,
            SumOfInvoiceItemValues = sumOfInvoiceItemValues,
            InvoiceItems = model.InvoiceItems
        };
        dbContext.Attach(result);
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
            return Error.NotFound(description: "Invoice not found.");
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
