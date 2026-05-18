using Microsoft.AspNetCore.Mvc;
using Solution.Services;
using Solution.Services.Models;
using System.ComponentModel.DataAnnotations;

namespace Solution.Api.Controllers;

public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost]
    [Route("api/login")]
    public async Task<IActionResult> LoginAsync([FromBody] [Required] CreateUserModel model)
    {
        return Ok(await userService.LoginUser(model));
    }
}
