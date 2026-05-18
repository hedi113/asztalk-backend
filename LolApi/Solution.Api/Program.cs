using Microsoft.EntityFrameworkCore;
using Solution.Database;
using Solution.Services;

var connectionString = "Data Source=.\\SQLEXPRESS; Database = ChampionsDb; Trusted_Connection = True; MultipleActiveResultSets = True; TrustServerCertificate = True; ";

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddTransient<IChampionService, ChampionService>();
builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
