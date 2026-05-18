using Microsoft.EntityFrameworkCore;
using Solution.Database.Entities;

namespace Solution.Database;

public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
{
    public DbSet<SpaceshipEntity> Spaceships { get; set; }
}
