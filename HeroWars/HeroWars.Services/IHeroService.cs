using HeroWars.Database.Entities;
using HeroWars.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeroWars.Services;

public interface IHeroService
{
    Task<HeroModel> CreateAsync(UpdateHeroModel requestModel);
    Task<bool> DeleteAsync(int id);
    Task<ICollection<HeroModel>> GetAllAsync();
    Task<HeroModel> GetByIdAsync(int id);
    Task<HeroModel> UpdateAsync(UpdateHeroModel model, int id);
}
