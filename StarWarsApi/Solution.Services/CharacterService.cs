using Microsoft.EntityFrameworkCore;
using Solution.Database;
using Solution.Database.Entities;
using Solution.Services.Models;
using System.Diagnostics.Metrics;

namespace Solution.Services;

public class CharacterService(AppDbContext dbContext) : ICharacterService
{
    public async Task<CharacterModel> CreateAsync(CharacterUpdateModel updateModel)
    {
        var entity = new CharacterEntity
        {
            Species = updateModel.Species,
            Rank = updateModel.Rank,
            OrderType = updateModel.OrderType,
            Master = updateModel.Master,
            LightSaberColor = updateModel.LightSaberColor,
            IsAlive = updateModel.IsAlive,
            Apprentice = updateModel.Apprentice,
            Era = updateModel.Era,
            ForceSpeciality = updateModel.ForceSpeciality,
            Homeworld = updateModel.Homeworld,
            Name = updateModel.Name,
        };
        dbContext.Add(entity);
        await dbContext.SaveChangesAsync();

        return Map(entity);
    }

    public async Task<CharacterModel> UpdateAsync(CharacterUpdateModel updateModel, int id)
    {
        var entity = await dbContext.Characters.SingleOrDefaultAsync(x => x.Id == id);
        if(entity is  null)
        {
            throw new Exception("Nincs ilyen karakter!");
        }

        entity.Species = updateModel.Species;
        entity.Rank = updateModel.Rank;
        entity.OrderType = updateModel.OrderType;
        entity.Master = updateModel.Master;
        entity.LightSaberColor = updateModel.LightSaberColor;
        entity.IsAlive = updateModel.IsAlive;
        entity.Apprentice = updateModel.Apprentice;
        entity.Era = updateModel.Era;
        entity.ForceSpeciality = updateModel.ForceSpeciality;
        entity.Homeworld = updateModel.Homeworld;
        entity.Id = updateModel.Id;
        entity.Name = updateModel.Name;

        await dbContext.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var rowAffected = await dbContext.Characters.Where(x => x.Id == id).ExecuteDeleteAsync();
        return rowAffected == 1;
    }

    public async Task<CharacterModel> GetByIdAsync(int id)
    {
        var entity = await dbContext.Characters.SingleOrDefaultAsync(x => x.Id == id);
        if (entity is null)
        {
            throw new Exception("Nincs ilyen karakter!");
        }

        return Map(entity);
    }

    public async Task<ICollection<CharacterModel>> GetAllAsync()
    {
        var characters = await dbContext.Characters.Select(x => Map(x)).ToListAsync();

        return characters;
    }

    private static CharacterModel Map(CharacterEntity entity)
    {
        return new CharacterModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Apprentice = entity.Apprentice,
            Era = entity.Era,
            ForceSpeciality = entity.ForceSpeciality,
            Homeworld = entity.Homeworld,
            IsAlive = entity.IsAlive,
            LightSaberColor = entity.LightSaberColor,
            Master = entity.Master,
            OrderType = entity.OrderType,
            Rank = entity.Rank,
            Species = entity.Species,
        };
    }
}
