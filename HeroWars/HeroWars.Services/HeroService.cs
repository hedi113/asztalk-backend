using HeroWars.Database;
using HeroWars.Database.Entities;
using HeroWars.Services.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace HeroWars.Services;

public class HeroService(AppDbContext dbContext) : IHeroService 
{
    public async Task<HeroModel> CreateAsync(UpdateHeroModel requestModel)
    {
        var entity = new HeroEntity
        {
            Agility = requestModel.Agility,
            Armor = requestModel.Armor,
            ArmorPenetration = requestModel.ArmorPenetration,
            Health = requestModel.Health,
            Intelligence = requestModel.Intelligence,
            MagicAttack = requestModel.MagicAttack,
            MagicDefense = requestModel.MagicDefense,
            MagicPenetration = requestModel.MagicPenetration,
            Name = requestModel.Name,
            PhysicalAttack = requestModel.PhysicalAttack,
            Role = requestModel.Role,
        };
        dbContext.Heroes.Add(entity);
        await dbContext.SaveChangesAsync();

        return Map(entity);
    }

    public async Task<HeroModel> UpdateAsync(UpdateHeroModel model, int id)
    {
        var entity = await dbContext.Heroes.SingleOrDefaultAsync(x => x.Id == id);

        if (entity is null)
        {
            throw new Exception($"Hero with id: {id} wasn't found!");
        }

        entity.Name = model.Name;
        entity.Role = model.Role;
        entity.Intelligence = model.Intelligence;
        entity.Agility = model.Agility;
        entity.Strength = model.Strength;
        entity.Health = model.Health;
        entity.PhysicalAttack = model.PhysicalAttack;
        entity.MagicAttack = model.MagicAttack;
        entity.Armor = model.Armor;
        entity.MagicDefense = model.MagicDefense;
        entity.MagicPenetration = model.MagicPenetration;

        await dbContext.SaveChangesAsync();

        return Map(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var rowAffected = await dbContext.Heroes.Where(x =>  x.Id == id).ExecuteDeleteAsync();

        return rowAffected == 1;
    }

    public async Task<HeroModel> GetByIdAsync(int id)
    {
        var entity = await dbContext.Heroes.SingleOrDefaultAsync(x => x.Id == id);
        if(entity is null)
        {
            throw new Exception($"Hero with id: {id} wasn't found!");
        }

        return Map(entity);
    }

    public async Task<ICollection<HeroModel>> GetAllAsync()
    {
        var heroes = await dbContext.Heroes.Select(x => Map(x)).ToListAsync();

        return heroes;
    }

    private static HeroModel Map(HeroEntity entity)
    {
        return new HeroModel
        {
            Id = entity.Id,
            Agility = entity.Agility,
            Armor = entity.Armor,
            ArmorPenetration = entity.ArmorPenetration,
            Health = entity.Health,
            Intelligence = entity.Intelligence,
            MagicAttack = entity.MagicAttack,
            MagicDefense = entity.MagicDefense,
            MagicPenetration = entity.MagicPenetration,
            Name = entity.Name,
            PhysicalAttack = entity.PhysicalAttack,
            Role = entity.Role,
        };
    }
}
