namespace Solution.Services;

public class CategoryService(AppDbContext dbContext) : ICategoryService
{
    private const int ROW_COUNT = 10;

    public async Task<ErrorOr<CategoryModel>> CreateAsync(CategoryModel model)
    {
        bool exists = await dbContext.Categories.AnyAsync(x => x.Name == model.Name);

        if (exists)
        {
            return Error.Conflict(description: "Category already exists!");
        }

        var category = model.ToEntity();

        await dbContext.Categories.AddAsync(category);
        await dbContext.SaveChangesAsync();

        return new CategoryModel(category)
        {
            Name = model.Name
        };
    }

    public async Task<ErrorOr<Success>> UpdateAsync(CategoryModel model)
    {
        var result = await dbContext.Categories.AsNoTracking()
                                                .Where(x => x.Id == model.Id)
                                                .ExecuteUpdateAsync(x => x.SetProperty(p => p.Name, model.Name));
        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<Success>> DeleteAsync(int categoryId)
    {
        var result = await dbContext.Categories.AsNoTracking()
                                                .Where(x => x.Id == categoryId)
                                                .ExecuteDeleteAsync();

        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<CategoryModel>> GetByIdAsync(int categoryId)
    {
        var category = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);

        if (category is null)
        {
            return Error.NotFound(description: "Category not found.");
        }

        return new CategoryModel(category);
    }

    public async Task<ErrorOr<List<CategoryModel>>> GetAllAsync() =>
        await dbContext.Categories.AsNoTracking()
                                   .Select(x => new CategoryModel(x))
                                   .ToListAsync();

    public async Task<ErrorOr<PaginationModel<CategoryModel>>> GetPagedAsync(int page = 0)
    {
        page = page < 0 ? 0 : page - 1;

        var categories = await dbContext.Categories.AsNoTracking()
                                                     .Skip(page * ROW_COUNT)
                                                     .Take(ROW_COUNT)
                                                     .Select(x => new CategoryModel(x))
                                                     .ToListAsync();

        var paginationModel = new PaginationModel<CategoryModel>
        {
            Items = categories,
            Count = await dbContext.Categories.CountAsync()
        };

        return paginationModel;
    }
}
