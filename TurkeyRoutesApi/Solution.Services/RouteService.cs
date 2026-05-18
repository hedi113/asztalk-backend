using Microsoft.EntityFrameworkCore;
using Solution.Database;
using Solution.Database.Entities;
using Solution.Services.Models;

namespace Solution.Services;

public class RouteService(AppDbContext dbContext) : IRouteService
{

    public async Task<RouteModel> CreateAsync(RouteUpdateModel updateModel)
    {
        var entity = new RouteEntity
        {
            ArrivalCity = updateModel.ArrivalCity,
            ArrivalHour = updateModel.ArrivalHour,
            ArrivalMinute = updateModel.ArrivalMinute,
            DepartureCity = updateModel.DepartureCity,
            DepartureHour = updateModel.DepartureHour,
            DepartureMinute = updateModel.DepartureMinute,
            DistanceKm = updateModel.DistanceKm,
        };

        dbContext.Routes.Add(entity);
        await dbContext.SaveChangesAsync();

        return Map(entity);

    }

    public async Task<RouteModel> UpdateAsync(RouteUpdateModel updateModel, int id)
    {
        var entity = await dbContext.Routes.SingleOrDefaultAsync(x => x.Id == id);
        if(entity == null)
        {
            throw new Exception("No route with such id!");
        }

        entity.ArrivalCity = updateModel.ArrivalCity;
        entity.ArrivalHour = updateModel.ArrivalHour;
        entity.ArrivalMinute = updateModel.ArrivalMinute;
        entity.DepartureCity = updateModel.DepartureCity;
        entity.DepartureHour = updateModel.DepartureHour;
        entity.DepartureMinute = updateModel.DepartureMinute;
        entity.DistanceKm = updateModel.DistanceKm;

        await dbContext.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var rowAffected = await dbContext.Routes.Where(x => x.Id == id).ExecuteDeleteAsync();
        return rowAffected == 1;
    }

    public async Task<RouteModel> GetByIdAsync(int id)
    {
        var entity = await dbContext.Routes.SingleOrDefaultAsync(x => x.Id == id);
        if (entity == null)
        {
            throw new Exception("No route with such id!");
        }

        return Map(entity);
    }

    public async Task<ICollection<RouteModel>> GetAllAsync()
    {
        var routes = await dbContext.Routes.Select(x => Map(x)).ToListAsync();
        return routes;
    }

    private static RouteModel Map(RouteEntity entity)
    {
        return new RouteModel
        {
            ArrivalCity = entity.ArrivalCity,
            ArrivalHour = entity.ArrivalHour,
            ArrivalMinute = entity.ArrivalMinute,
            DepartureCity = entity.DepartureCity,
            DepartureHour = entity.DepartureHour,
            DepartureMinute = entity.DepartureMinute,
            DistanceKm = entity.DistanceKm,
            Id = entity.Id,
        };
    }
}
