using Solution.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.Services;

public interface IChampionService
{
    Task<ChampionModel> CreateAsync(EditChampionModel model);
    Task<bool> DeleteAsync(int id);
    Task<ICollection<ChampionModel>> GetAllAsync();
    Task<ChampionModel> GetByIdAsync(int id);
}
