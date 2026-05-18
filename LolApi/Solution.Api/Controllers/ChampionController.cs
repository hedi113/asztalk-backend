using Microsoft.AspNetCore.Mvc;
using Solution.Services;
using Solution.Services.Models;
using System.ComponentModel.DataAnnotations;

namespace Solution.Api.Controllers;

[ApiController]
public class ChampionController(IChampionService championService) : ControllerBase
{
    [HttpPost]
    [Route("api/champions")]
    public async Task<IActionResult> CreateAsync([FromBody] [Required] EditChampionModel model)
    {
        return Ok(await championService.CreateAsync(model));
    }
    [HttpDelete]
    [Route("api/champions/{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        return Ok(await championService.DeleteAsync(id));
    }
    [HttpGet]
    [Route("api/champions")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await championService.GetAllAsync());
    }
    [HttpGet]
    [Route("api/champions/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute][Required] int id) 
    {
        return Ok(await championService.GetByIdAsync(id));
    }
}
