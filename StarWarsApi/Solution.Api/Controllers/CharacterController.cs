using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using Solution.Services;
using Solution.Services.Models;
using System.ComponentModel.DataAnnotations;

namespace Solution.Api.Controllers;

[ApiController]
public class CharacterController(ICharacterService characterService) : ControllerBase
{
    [HttpPost("api/create/character")]
    public async Task<IActionResult> CreateAsync([FromBody] [Required] CharacterUpdateModel updateModel)
    {
        return Ok(await characterService.CreateAsync(updateModel));
    }
    [HttpDelete("api/delete/{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] [Required] int id)
    {
        return Ok(await characterService.DeleteAsync(id));
    }
    [HttpGet("api/characters")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await characterService.GetAllAsync());
    }
    [HttpGet("api/character/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] [Required] int id)
    {
        return Ok(await characterService.GetByIdAsync(id));
    }
    [HttpPut("api/update/{id}")]
    public async Task<IActionResult> UpdateAsync([FromBody][Required] CharacterUpdateModel updateModel, [FromRoute][Required] int id)
    {
        return Ok(await characterService.UpdateAsync(updateModel, id));
    }
}
