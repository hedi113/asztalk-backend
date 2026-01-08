using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Solution.Domain.Database.Entities;
using Solution.Domain.Models.Views;

namespace Solution.Services.User;

public class UserService(UserManager<UserEntity> userManager): IUserService
{
    public async Task<ErrorOr<ICollection<UserModel>>> GetAllUsers()
    {
        return await userManager.Users.Select(x => new UserModel
        {
            Email = x.Email,
            Name = x.FullName,
        })
        .ToListAsync();
    }
}
