namespace Solution.DesktopApp.Configurations;

public static class ConfigureSQLServer
{
	public static MauiAppBuilder UseMsSqlServer(this MauiAppBuilder builder)
	{
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseLazyLoadingProxies()
                    .UseSqlServer(connectionString, options =>
                    {
                        options.MigrationsAssembly(AssemblyReference.Assembly);
                        options.EnableRetryOnFailure();
                        options.CommandTimeout(300);
                    })
        );

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        return builder;
    }
}
