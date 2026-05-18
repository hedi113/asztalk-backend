using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using Solution.Database;
using Solution.Database.Entities;
using Solution.Services.Models;
using System.Data;
using System.Security.Claims;

namespace Solution.Services;

public class SpaceshipService(AppDBContext dbContext) : ISpaceshipService
{
    public async Task<SpaceshipModel> CreateAsync(UpdateSpaceshipModel spaceshipModel)
    {
        var entity = new SpaceshipEntity
        {
            Armament = spaceshipModel.Armament,
            Class = spaceshipModel.Class,
            Crew = spaceshipModel.Crew,
            HullMaterial = spaceshipModel.HullMaterial,
            Length = spaceshipModel.Length,
            MaxWarp = spaceshipModel.MaxWarp,
            Name = spaceshipModel.Name,
            RaceFaction = spaceshipModel.RaceFaction,
            Role = spaceshipModel.Role,
            ShieldType = spaceshipModel.ShieldType,
        };
        dbContext.Add(entity);
        await dbContext.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<SpaceshipModel> UpdateAsync(UpdateSpaceshipModel spaceshipModel, int id)
    {
        var entity = await dbContext.Spaceships.SingleOrDefaultAsync(x => x.Id == id);
        if (entity == null)
        {
            throw new Exception("No such entity with this id!");
        }

        entity.Armament = spaceshipModel.Armament;
        entity.Class = spaceshipModel.Class;
        entity.Crew = spaceshipModel.Crew;
        entity.HullMaterial = spaceshipModel.HullMaterial;
        entity.Length = spaceshipModel.Length;
        entity.MaxWarp = spaceshipModel.MaxWarp;
        entity.Name = spaceshipModel.Name;
        entity.RaceFaction = spaceshipModel.RaceFaction;
        entity.Role = spaceshipModel.Role;
        entity.ShieldType = spaceshipModel.ShieldType;

        await dbContext.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var rowAffected = await dbContext.Spaceships.Where(x => x.Id == id).ExecuteDeleteAsync();
        return rowAffected == 1;
    }

    public async Task<SpaceshipModel> GetByIdAsync(int id)
    {
        var entity = await dbContext.Spaceships.SingleOrDefaultAsync(x => x.Id == id);
        if (entity == null)
        {
            throw new Exception("No such entity with this id!");
        }
        return Map(entity);
    }

    public async Task<ICollection<SpaceshipModel>> GetAllAsync()
    {
        var spaceships = await dbContext.Spaceships.Select(x => Map(x)).ToListAsync();
        return spaceships;
    }

    private static SpaceshipModel Map(SpaceshipEntity entity)
    {
        return new SpaceshipModel
        {
            Armament = entity.Armament,
            Class = entity.Class,
            Crew = entity.Crew,
            HullMaterial = entity.HullMaterial,
            Id = entity.Id,
            Length = entity.Length,
            MaxWarp = entity.MaxWarp,
            Name = entity.Name,
            RaceFaction = entity.RaceFaction,
            Role = entity.Role,
            ShieldType = entity.ShieldType,
        };
    }
}
