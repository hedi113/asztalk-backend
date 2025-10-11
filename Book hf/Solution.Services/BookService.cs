namespace Solution.Services;

public class BookService(AppDbContext dbContext) : IBookService
{
    private const int ROW_COUNT = 10;

    public async Task<ErrorOr<BookModel>> CreateAsync(BookModel model)
    {
        bool exists = await dbContext.Books.AnyAsync(x => x.AuthorId == model.Author.Id &&
                                                                x.Title.ToLower() == model.Title.ToLower().Trim() &&
                                                                x.ReleaseDate == model.ReleaseDate);

        if (exists)
        {
            return Error.Conflict(description: "Book already exists!");
        }

        var book = model.ToEntity();
        book.PublicId = Guid.NewGuid().ToString();
        
        await dbContext.Books.AddAsync(book);
        await dbContext.SaveChangesAsync();

        return new BookModel(book)
        {
            Author = model.Author
        };
    }

    public async Task<ErrorOr<Success>> UpdateAsync(BookModel model)
    {
        var result = await dbContext.Books.AsNoTracking()
                                                .Where(x => x.PublicId == model.PublicId)
                                                .ExecuteUpdateAsync(x => x.SetProperty(p => p.PublicId, model.PublicId)
                                                                          .SetProperty(p => p.AuthorId, model.Author.Id)
                                                                          .SetProperty(p => p.CategoryId, model.Category.Id)
                                                                          .SetProperty(p => p.Title, model.Title)
                                                                          .SetProperty(p => p.PageNumber, model.PageNumber)
                                                                          .SetProperty(p => p.ReleaseDate, model.ReleaseDate)
                                                                          .SetProperty(p => p.Publisher, model.Publisher)
                                                                          .SetProperty(p => p.ImageId, model.ImageId)
                                                                          .SetProperty(p => p.WebContentLink, model.WebContentLink));
        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<Success>> DeleteAsync(string bookId)
    {
        var result = await dbContext.Books.AsNoTracking()
                                                .Include(x => x.Author)
                                                .Include(x => x.Category)
                                                .Where(x => x.PublicId == bookId)
                                                .ExecuteDeleteAsync();

        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<BookModel>> GetByIdAsync(string bookId)
    {
        var book = await dbContext.Books.Include(x => x.Author)
                                                    .FirstOrDefaultAsync(x => x.PublicId == bookId);

        if (book is null)
        {
            return Error.NotFound(description: "Book not found.");
        }

        return new BookModel(book);
    }

    public async Task<ErrorOr<List<BookModel>>> GetAllAsync() =>
        await dbContext.Books.AsNoTracking()
                                   .Include(x => x.Author)
                                   .Include(x => x.Category)
                                   .Select(x => new BookModel(x))
                                   .ToListAsync();

    public async Task<ErrorOr<PaginationModel<BookModel>>> GetPagedAsync(int page = 0)
    {
        page = page <= 0 ? 1 : page - 1;

        var books = await dbContext.Books.AsNoTracking()
                                                     .Include(x => x.Author)
                                                     .Include(x => x.Category)
                                                     .Skip(page * ROW_COUNT)
                                                     .Take(ROW_COUNT)
                                                     .Select(x => new BookModel(x))
                                                     .ToListAsync();

        var paginationModel = new PaginationModel<BookModel>
        {
            Items = books,
            Count = await dbContext.Books.CountAsync()
        };

        return paginationModel;
    }
}
