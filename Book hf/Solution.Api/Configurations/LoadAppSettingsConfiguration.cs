namespace Solution.Api.Configurations;

public static class LoadAppSettingsConfiguration
{
    public static WebApplicationBuilder LoadAppSettingsVariables(this WebApplicationBuilder builder)
    {
        var enviroment = builder.Configuration.GetValue<string>("Enviroment");

        builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                             .AddJsonFile("appsettings.json", true)
                             .AddJsonFile($"appsettings.{enviroment}.json", true)
                             .AddEnvironmentVariables();

        return builder;
    }
}
