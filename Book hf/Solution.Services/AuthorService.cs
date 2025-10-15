

namespace Solution.Services;

public class AuthorService(AppDbContext dbContext) : IAuthorService
{
    private const int ROW_COUNT = 10;

    public async Task<ErrorOr<AuthorModel>> CreateAsync(AuthorModel model)
    {
        bool exists = await dbContext.Authors.AnyAsync(x => x.Name == model.Name && x.BirthYear == model.BirthYear);

        if (exists)
        {
            return Error.Conflict(description: "Author already exists!");
        }

        var author = model.ToEntity();

        await dbContext.Authors.AddAsync(author);
        await dbContext.SaveChangesAsync();

        return new AuthorModel(author)
        {
            Name = model.Name
        };
    }

    public async Task<ErrorOr<Success>> UpdateAsync(AuthorModel model)
    {
        var result = await dbContext.Authors.AsNoTracking()
                                                .Where(x => x.Id == model.Id)
                                                .ExecuteUpdateAsync(x => x.SetProperty(p => p.Name, model.Name).SetProperty(p => p.BirthYear, model.BirthYear));
        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<Success>> DeleteAsync(int typeId)
    {
        var result = await dbContext.Authors.AsNoTracking()
                                                .Where(x => x.Id == typeId)
                                                .ExecuteDeleteAsync();

        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<AuthorModel>> GetByIdAsync(int typeId)
    {
        var author = await dbContext.Authors.FirstOrDefaultAsync(x => x.Id == typeId);

        if (author is null)
        {
            return Error.NotFound(description: "Author not found.");
        }

        return new AuthorModel(author);
    }

    public async Task<ErrorOr<List<AuthorModel>>> GetAllAsync() =>
        await dbContext.Authors.AsNoTracking()
                                   .Select(x => new AuthorModel(x))
                                   .ToListAsync();

    public async Task<ErrorOr<PaginationModel<AuthorModel>>> GetPagedAsync(int page = 0)
    {
        page = page < 0 ? 0 : page - 1;

        var authors = await dbContext.Authors.AsNoTracking()
                                                     .Skip(page * ROW_COUNT)
                                                     .Take(ROW_COUNT)
                                                     .Select(x => new AuthorModel(x))
                                                     .ToListAsync();

        var paginationModel = new PaginationModel<AuthorModel>
        {
            Items = authors,
            Count = await dbContext.Authors.CountAsync()
        };

        return paginationModel;
    }
}
