using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solution.Domain.Models.Views;
using Solution.Services.User;
using Solution.Shared.Extensions;
using System.ComponentModel;

namespace Solution.WebAPI.Controllers;

[ApiController]
[ProducesResponseType(statusCode: 400, type: typeof(BadRequestObjectResult))]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet]
    [Route("api/users")]
    [Authorize]
    [ProducesResponseType(type: typeof(ICollection<UserModel>), statusCode: 200)]
    [EndpointDescription("This endpoint will return all users from the database.")]
    public async Task<IActionResult> GetUserAsync()
    {
        var result = await userService.GetAllUsers();
        return result.Match(
            value => Ok(value),
            errors => errors.ToProblemResult()
        );
    }
}
