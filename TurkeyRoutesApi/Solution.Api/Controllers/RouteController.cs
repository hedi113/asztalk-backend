using Microsoft.AspNetCore.Mvc;
using Solution.Services;
using Solution.Services.Models;
using System.ComponentModel.DataAnnotations;

namespace Solution.Api.Controllers;

[ApiController]
public class RouteController(IRouteService routeService) : ControllerBase
{
    [HttpPost("api/route/create")]
    public async Task<IActionResult> CreateAsync([FromBody] [Required] RouteUpdateModel updateModel)
    {
        return Ok(await routeService.CreateAsync(updateModel));
    }

    [HttpPut("api/route/delete/{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] [Required] int id)
    {
        return Ok(await routeService.DeleteAsync(id));
    }

    [HttpGet("api/routes")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await routeService.GetAllAsync());
    }

    [HttpGet("api/route/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] [Required] int id)
    {
        return Ok(await routeService.GetByIdAsync(id));
    }

    [HttpPut("api/route/update/{id}")]
    public async Task<IActionResult> UpdateAsync([FromBody] [Required] RouteUpdateModel updateModel, [FromRoute] [Required] int id)
    {
        return Ok(await routeService.UpdateAsync(updateModel, id));
    }
}
