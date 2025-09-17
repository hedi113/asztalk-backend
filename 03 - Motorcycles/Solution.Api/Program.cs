var builder = WebApplication.CreateBuilder(args);

builder.LoadAppSettingsVariables()
       .ConfigureDatabase();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
