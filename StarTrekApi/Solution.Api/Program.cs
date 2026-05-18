using Microsoft.EntityFrameworkCore;
using Solution.Database;
using Solution.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Server=(localdb)\\mssqllocaldb;Database=StarTrekDB;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddTransient<ISpaceshipService, SpaceshipService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
