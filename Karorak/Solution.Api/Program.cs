using Microsoft.EntityFrameworkCore;
using Solution.Database;
using Solution.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Server=(localdb)\\mssqllocaldb;Database=WatchesDb;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;";

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddTransient<IWatchService, WatchService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
