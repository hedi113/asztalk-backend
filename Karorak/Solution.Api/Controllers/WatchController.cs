using Microsoft.AspNetCore.Mvc;
using Solution.Services;
using Solution.Services.Models;
using System.ComponentModel.DataAnnotations;

namespace Solution.Api.Controllers;

[ApiController]
public class WatchController(IWatchService watchService) : ControllerBase
{
    [HttpPost("api/watch")]
    public async Task<IActionResult> CreateAsync([FromBody] [Required] WatchUpdateModel updateModel)
    {
        return Ok(await watchService.CreateAsync(updateModel));
    }

    [HttpDelete("api/watch/delete/{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] [Required] int id)
    {
        return Ok(await watchService.DeleteAsync(id));
    }

    [HttpGet("api/watches")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await watchService.GetAllAsync());
    }

    [HttpGet("api/watch/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] [Required] int id)
    {
        return Ok(await watchService.GetByIdAsync(id));
    }

    [HttpPut("api/watch/update/{id}")]
    public async Task<IActionResult> UpdateAsync([FromBody] [Required] WatchUpdateModel updateModel, [FromRoute] [Required] int id)
    {
        return Ok(await watchService.UpdateAsync(updateModel, id));
    }
}
