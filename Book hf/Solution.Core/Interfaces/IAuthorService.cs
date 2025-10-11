namespace Solution.Core.Interfaces;

public interface IAuthorService
{
    Task<ErrorOr<AuthorModel>> CreateAsync(AuthorModel model);
    Task<ErrorOr<Success>> UpdateAsync(AuthorModel model);
    Task<ErrorOr<Success>> DeleteAsync(int typeId);
    Task<ErrorOr<AuthorModel>> GetByIdAsync(int typeId);
    Task<ErrorOr<List<AuthorModel>>> GetAllAsync();
    Task<ErrorOr<PaginationModel<AuthorModel>>> GetPagedAsync(int page = 0);
}
