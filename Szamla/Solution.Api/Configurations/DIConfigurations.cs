namespace Solution.Api.Configurations;

public static class DIConfigurations
{
    public static WebApplicationBuilder ConfigureDI(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddTransient<IInvoiceService, InvoiceService>();

        builder.Services.AddTransient<IInvoiceItemService, InvoiceItemService>();

        return builder;
    }
}