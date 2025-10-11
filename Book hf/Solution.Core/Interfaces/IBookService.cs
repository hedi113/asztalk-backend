namespace Solution.Core.Interfaces;

public interface IBookService
{
    Task<ErrorOr<BookModel>> CreateAsync(BookModel model);
    Task<ErrorOr<Success>> UpdateAsync(BookModel model);
    Task<ErrorOr<Success>> DeleteAsync(string motorcycleId);
    Task<ErrorOr<BookModel>> GetByIdAsync(string motorcycleId);
    Task<ErrorOr<List<BookModel>>> GetAllAsync();
    Task<ErrorOr<PaginationModel<BookModel>>> GetPagedAsync(int page = 0);
}
