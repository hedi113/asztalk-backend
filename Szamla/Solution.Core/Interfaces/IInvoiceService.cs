namespace Solution.Core.Interfaces;

public interface IInvoiceService 
{
    Task<ErrorOr<InvoiceModel>> CreateAsync(InvoiceModel model);
    Task<ErrorOr<Success>> UpdateAsync(InvoiceModel model);
    Task<ErrorOr<Success>> DeleteAsync(int invoiceId);
    Task<ErrorOr<InvoiceModel>> GetByIdAsync(int invoiceId);
    Task<ErrorOr<List<InvoiceModel>>> GetAllAsync();
    Task<ErrorOr<PaginationModel<InvoiceModel>>> GetPagedAsync(int page = 0);
}
