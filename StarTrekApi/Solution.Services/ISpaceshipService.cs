using Solution.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.Services;

public interface ISpaceshipService
{
    Task<SpaceshipModel> CreateAsync(UpdateSpaceshipModel spaceshipModel);
    Task<bool> DeleteAsync(int id);
    Task<ICollection<SpaceshipModel>> GetAllAsync();
    Task<SpaceshipModel> GetByIdAsync(int id);
    Task<SpaceshipModel> UpdateAsync(UpdateSpaceshipModel spaceshipModel, int id);
}
