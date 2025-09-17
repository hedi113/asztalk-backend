namespace Solution.Core.Interfaces;

public interface IManufacturerService
{
    Task<ErrorOr<ManufacturerModel>> CreateAsync(ManufacturerModel model);
    Task<ErrorOr<Success>> UpdateAsync(ManufacturerModel model);
    Task<ErrorOr<Success>> DeleteAsync(int typeId);
    Task<ErrorOr<ManufacturerModel>> GetByIdAsync(int typeId);
    Task<ErrorOr<List<ManufacturerModel>>> GetAllAsync();
    Task<ErrorOr<PaginationModel<ManufacturerModel>>> GetPagedAsync(int page = 0);
}
