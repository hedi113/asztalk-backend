namespace Solution.Core.Interfaces;

public interface ICategoryService
{
    Task<ErrorOr<CategoryModel>> CreateAsync(CategoryModel model);
    Task<ErrorOr<Success>> UpdateAsync(CategoryModel model);
    Task<ErrorOr<Success>> DeleteAsync(int typeId);
    Task<ErrorOr<CategoryModel>> GetByIdAsync(int typeId);
    Task<ErrorOr<List<CategoryModel>>> GetAllAsync();
    Task<ErrorOr<PaginationModel<CategoryModel>>> GetPagedAsync(int page = 0);
}
