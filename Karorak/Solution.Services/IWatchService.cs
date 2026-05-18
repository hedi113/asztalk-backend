using Solution.Services.Models;

namespace Solution.Services;

public interface IWatchService
{
    Task<WatchModel> CreateAsync(WatchUpdateModel updateModel);
    Task<bool> DeleteAsync(int id);
    Task<ICollection<WatchModel>> GetAllAsync();
    Task<WatchModel> GetByIdAsync(int id);
    Task<WatchModel> UpdateAsync(WatchUpdateModel updateModel, int id);
}
