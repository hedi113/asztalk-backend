namespace Solution.WebAPI.Configurations;

public static class LoadEnviromentVariablesConfiguration
{
    extension(IHostApplicationBuilder builder)
    {
        public IHostApplicationBuilder LoadEnviromentVariables()
        {
            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                 .AddEnvironmentVariables();
            return builder;
        }
    }
}
