using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using Solution.Services;
using Solution.Services.Models;
using System.ComponentModel.DataAnnotations;

namespace Solution.Api.Controllers;

[ApiController]
public class SpaceshipController(ISpaceshipService spaceshipService) : ControllerBase
{
    [HttpPost("/api/spaceship")]
    public async Task<IActionResult> CreateAsync([FromBody] [Required] UpdateSpaceshipModel spaceshipModel)
    {
        return Ok(await spaceshipService.CreateAsync(spaceshipModel));
    }
    [HttpPut("api/spaceship/update/{id}")]
    public async Task<IActionResult> UpdateAsync([FromBody][Required] UpdateSpaceshipModel spaceshipModel, [FromRoute][Required] int id) 
    {
        return Ok(await spaceshipService.UpdateAsync(spaceshipModel, id));
    }
    [HttpDelete("api/spaceship/delete/{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] [Required] int id)
    {
        return Ok(await spaceshipService.DeleteAsync(id));
    }
    [HttpGet("api/spaceships")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await spaceshipService.GetAllAsync());
    }
    [HttpGet("api/spaceship/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] [Required] int id)
    {
        return Ok(await spaceshipService.GetByIdAsync(id));
    }
}
