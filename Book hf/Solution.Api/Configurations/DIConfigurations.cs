namespace Solution.Api.Configurations;

public static class DIConfigurations
{
    public static WebApplicationBuilder ConfigureDI(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddTransient<IBookService, BookService>();

        builder.Services.AddTransient<IAuthorService, AuthorService>();

        builder.Services.AddTransient<ICategoryService, CategoryService>();

        return builder;
    }
}