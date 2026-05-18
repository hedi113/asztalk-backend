using Microsoft.EntityFrameworkCore;
using Solution.Database;
using Solution.Database.Entities;
using Solution.Services.Models;

namespace Solution.Services;

public class ChampionService(AppDbContext dbContext) : IChampionService
{
    public async Task<ChampionModel> CreateAsync(EditChampionModel model)
    {
        if(dbContext.Users == null)
        {
            throw new Exception("Not logged in!");
        }
        else
        {
            var entity = new ChampionEntity
            {
                Role = model.Role,
                Name = model.Name,
                Lane = model.Lane,
                Difficulity = model.Difficulity,
                Description = model.Description,
                DamageType = model.DamageType,
                BlueEssence = model.BlueEssence,
            };

            dbContext.Add(entity);
            await dbContext.SaveChangesAsync();

            return Map(entity);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if(dbContext.Users == null)
        {
            throw new Exception("Not logged in!");
        }
        else
        {
            var rowAffected = await dbContext.Champions.Where(x => x.Id == id).ExecuteDeleteAsync();

            return rowAffected == 1;
        }
    }

    public async Task<ChampionModel> GetByIdAsync(int id)
    {
        var entity = await dbContext.Champions.SingleOrDefaultAsync(x => x.Id == id);
        if(entity == null)
        {
            throw new Exception("Champion wasn't found!");
        }

        return Map(entity);
    }

    public async Task<ICollection<ChampionModel>> GetAllAsync()
    {
        var champions = await dbContext.Champions.Select(x => Map(x)).ToListAsync();

        return champions;
    }

    public static ChampionModel Map(ChampionEntity entity)
    {
        return new ChampionModel
        {
            Id = entity.Id,
            BlueEssence = entity.BlueEssence,
            DamageType = entity.DamageType,
            Description = entity.Description,
            Difficulity = entity.Difficulity,
            Lane = entity.Lane,
            Name = entity.Name,
            Role = entity.Role,
        };
    }
}
