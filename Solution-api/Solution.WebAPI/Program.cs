var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.LoadEnviromentVariables()
       .ConfigureDatabase()
       .LoadSettings()
       .UseSecurity()
       .UseIdentity()
       .ConfigureDI();

var app = builder.Build();


app.UseHttpsRedirection();

app.UseRouting();

app.UseSecurity();

app.MapControllers();

app.Run();
