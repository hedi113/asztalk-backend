using Microsoft.EntityFrameworkCore;
using Solution.Database.Entities;

namespace Solution.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<RouteEntity> Routes { get; set; }
}
