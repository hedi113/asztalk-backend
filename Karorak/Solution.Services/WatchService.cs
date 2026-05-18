using Microsoft.EntityFrameworkCore;
using Solution.Database;
using Solution.Database.Entities;
using Solution.Services.Models;

namespace Solution.Services;

public class WatchService(AppDbContext dbContext) : IWatchService
{
    public async Task<WatchModel> CreateAsync(WatchUpdateModel updateModel)
    {
        var entity = new WatchEntity
        {
            CaseMaterial = updateModel.CaseMaterial,
            Category = updateModel.Category,
            Functions = updateModel.Functions,
            Manufacturer = updateModel.Manufacturer,
            Model = updateModel.Model,
            Movement = updateModel.Movement,
            ReleaseYear = updateModel.ReleaseYear,
            Type = updateModel.Type,
            WaterResistanceM = updateModel.WaterResistanceM,
        };

        dbContext.Watches.Add(entity);
        await dbContext.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<WatchModel> UpdateAsync(WatchUpdateModel updateModel, int id)
    {
        var entity = await dbContext.Watches.SingleOrDefaultAsync(w => w.Id == id);
        if(entity == null)
        {
            throw new Exception("Watch not found!");
        }

        entity.CaseMaterial = updateModel.CaseMaterial;
        entity.Category = updateModel.Category;
        entity.Functions = updateModel.Functions;
        entity.Manufacturer = updateModel.Manufacturer;
        entity.Model = updateModel.Model;
        entity.Movement = updateModel.Movement;
        entity.ReleaseYear = updateModel.ReleaseYear;
        entity.Type = updateModel.Type;
        entity.WaterResistanceM = updateModel.WaterResistanceM;

        await dbContext.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var rowAffected = await dbContext.Watches.Where(x => x.Id == id).ExecuteDeleteAsync();
        return rowAffected == 1;
    }

    public async Task<ICollection<WatchModel>> GetAllAsync()
    {
        var watches = await dbContext.Watches.Select(x => Map(x)).ToListAsync();
        return watches;
    }

    public async Task<WatchModel> GetByIdAsync(int id)
    {
        var entity = await dbContext.Watches.SingleOrDefaultAsync(w => w.Id == id);
        if (entity == null)
        {
            throw new Exception("Watch not found!");
        }

        return Map(entity);
    }

    public static WatchModel Map(WatchEntity entity)
    {
        return new WatchModel
        {
            Id = entity.Id,
            CaseMaterial = entity.CaseMaterial,
            Category = entity.Category,
            Functions = entity.Functions,
            Manufacturer = entity.Manufacturer,
            Model = entity.Model,
            Movement = entity.Movement,
            ReleaseYear = entity.ReleaseYear,
            Type = entity.Type,
            WaterResistanceM = entity.WaterResistanceM,
        };
    }
}
