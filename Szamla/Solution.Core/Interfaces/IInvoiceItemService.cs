namespace Solution.Core.Interfaces;

public interface IInvoiceItemService
{
    Task<ErrorOr<InvoiceItemModel>> CreateAsync(InvoiceItemModel model);
    Task<ErrorOr<Success>> UpdateAsync(InvoiceItemModel model);
    Task<ErrorOr<Success>> DeleteAsync(int itemId);
    Task<ErrorOr<InvoiceItemModel>> GetByIdAsync(int itemId);
    Task<ErrorOr<List<InvoiceItemModel>>> GetAllAsync();
    Task<ErrorOr<PaginationModel<InvoiceItemModel>>> GetPagedAsync(int page = 0);
}
