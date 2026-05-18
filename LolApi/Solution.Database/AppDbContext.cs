using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Solution.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ChampionEntity> Champions { get; set; }
    public DbSet<UserEntity> Users { get; set; }
}
