using HeroWars.Services;
using HeroWars.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HeroWarsApi.Controllers;

[ApiController]
public class HeroController(IHeroService heroService) : ControllerBase
{
    [HttpPost]
    [Route("api/hero")]
    public async Task<IActionResult> CreateAsync([FromBody] [Required] UpdateHeroModel requestModel) 
    {
        var hero = await heroService.CreateAsync(requestModel);
        return Ok(hero);
    }

    [HttpDelete]
    [Route("api/hero/{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] [Required] int id)
    {
        var result = await heroService.DeleteAsync(id);
        return Ok(result);
    }

    [HttpGet]
    [Route("api/heroes")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await heroService.GetAllAsync());
    }

    [HttpGet]
    [Route("api/hero/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute][Required] int id)
    {
        return Ok(await heroService.GetByIdAsync(id));
    }

    [HttpPut]
    [Route("api/hero/{id}/update")]
    public async Task<IActionResult> UpdateAsync([FromBody][Required] UpdateHeroModel model, [FromRoute][Required] int id)
    {
        return Ok(await heroService.UpdateAsync(model, id));
    }
}
