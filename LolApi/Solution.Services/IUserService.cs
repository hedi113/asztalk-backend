using Solution.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.Services;

public interface IUserService
{
    Task<UserModel> LoginUser(CreateUserModel model);
}
