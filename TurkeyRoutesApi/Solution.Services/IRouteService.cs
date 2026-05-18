using Solution.Services.Models;

namespace Solution.Services;

public interface IRouteService
{
    Task<RouteModel> CreateAsync(RouteUpdateModel updateModel);
    Task<bool> DeleteAsync(int id);
    Task<ICollection<RouteModel>> GetAllAsync();
    Task<RouteModel> GetByIdAsync(int id);
    Task<RouteModel> UpdateAsync(RouteUpdateModel updateModel, int id);
}
