using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solution.Services.User;
using Solution.Shared.Extensions;

namespace Solution.WebAPI.Controllers;

[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet]
    [Route("api/users")]
    [Authorize]
    public async Task<IActionResult> GetUserAsync()
    {
        var result = await userService.GetAllUsers();
        return result.Match(
            value => Ok(value),
            errors => errors.ToProblemResult()
        );
    }
}
