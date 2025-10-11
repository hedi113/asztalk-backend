using Solution.Services.Services;

namespace Solution.DesktopApp.Configurations;

public static class ConfigureDI
{
	public static MauiAppBuilder UseDIConfiguration(this MauiAppBuilder builder)
	{
		builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<BookListViewModel>();
        builder.Services.AddTransient<AuthorListViewModel>();
        builder.Services.AddTransient<CategoryListViewModel>();
        builder.Services.AddTransient<AddBookViewModel>();
        builder.Services.AddTransient<AddCategoryViewModel>();
        builder.Services.AddTransient<AddAuthorViewModel>();

        builder.Services.AddTransient<MainView>();
        builder.Services.AddTransient<BookListView>();
        builder.Services.AddTransient<AuthorListView>();
        builder.Services.AddTransient<CategoryListView>();
        builder.Services.AddTransient<AddBookView>();
        builder.Services.AddTransient<AddAuthorView>();
        builder.Services.AddTransient<AddCategoryView>();

        builder.Services.AddScoped<IGoogleDriveService, GoogleDriveService> ();
        builder.Services.AddTransient<IBookService, BookService>();
        builder.Services.AddTransient<IAuthorService, AuthorService>();
        builder.Services.AddTransient<ICategoryService, CategoryService>();

        return builder;
	}
}
