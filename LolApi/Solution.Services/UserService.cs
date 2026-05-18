using Solution.Database;
using Solution.Database.Entities;
using Solution.Services.Models;

namespace Solution.Services;

public class UserService(AppDbContext dbContext) : IUserService
{
    public async Task<UserModel> LoginUser(CreateUserModel model)
    {
        var entity = new UserEntity
        {
            Id = model.Id,
            UserName = model.UserName,
            Password = model.Password,
        };

        dbContext.Users.Add(entity);
        await dbContext.SaveChangesAsync();

        return new UserModel
        {
            Id = model.Id,
            UserName = model.UserName,
            Password = model.Password,
        };
    }
}
