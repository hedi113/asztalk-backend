using ErrorOr;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Solution.Domain.Models.Requests.Security;
using Solution.Domain.Models.Responses;
using Solution.Services.Security;
using Solution.Shared.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Solution.WebAPI.Controllers;

[ApiController]
[ProducesResponseType(statusCode: 400, type: typeof(BadRequestObjectResult))]
public class SecurityController(ISecurityService securityService) : ControllerBase
{
    [HttpPost]
    [Route("/api/security/register")]
    [ProducesResponseType(type: typeof(Success), statusCode: 200)]
    [EndpointDescription("Register a user using an email and a password.")]
    public async Task<IActionResult> RegisterAsync([FromBody][Required] RegisterRequestModel model)
    {
        var result = await securityService.RegisterAsync(model);
        return result.Match(
            value => Ok(value),
            errors => errors.ToProblemResult()
        );
    }

    [HttpPost]
    [Route("/api/security/login")]
    [EndpointDescription("Login using an email and a password.")]
    [ProducesResponseType(type: typeof(TokenResponseModel), statusCode: 200)]
    public async Task<IActionResult> LoginAsync([FromBody][Required] LoginRequestModel model)
    {
        var result = await securityService.LoginAsync(model);
        return result.Match(
            value => Ok(value),
            errors => errors.ToProblemResult()
        );
    }
}
