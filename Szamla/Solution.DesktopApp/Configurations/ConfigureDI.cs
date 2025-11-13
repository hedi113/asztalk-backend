using Solution.DesktopApp.Views;
using Solution.Services.Services;

namespace Solution.DesktopApp.Configurations;

public static class ConfigureDI
{
	public static MauiAppBuilder UseDIConfiguration(this MauiAppBuilder builder)
	{
		builder.Services.AddTransient<MainViewModel>();

		builder.Services.AddTransient<InvoiceViewModel>();

        builder.Services.AddTransient<MainView>();
        
		builder.Services.AddTransient<InvoiceView>();

        builder.Services.AddScoped<IGoogleDriveService, GoogleDriveService> ();
        
		builder.Services.AddScoped<IInvoiceService, InvoiceService> ();

        return builder;
	}
}
