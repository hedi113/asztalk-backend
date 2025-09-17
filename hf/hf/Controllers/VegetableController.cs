using hf.Models;
using System.ComponentModel.DataAnnotations;

namespace hf.Controllers;

public class VegetableController : ControllerBase
{
    public List<string> Vegetables = [
        "Lettuce",
        "Squash",
        "Tomato"
        ];

    [HttpGet]
    [Route("vegetable/id/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute][Required] int id)
    {
        return Ok(Vegetables[id]);
    }

    [HttpGet]
    [Route("vegetable/all")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(Vegetables);
    }

    [HttpPost]
    [Route("vegetable/create")]
    public async Task<IActionResult> CreateAsync([FromBody][Required] string motorcycle)
    {
        Vegetables.Add(motorcycle);

        return Ok(Vegetables);
    }

    [HttpPut]
    [Route("vegetable/update")]
    public async Task<IActionResult> UpdateAsync([FromBody][Required] VegetableUpdateModel model)
    {
        Vegetables[model.Id] = model.Name;

        return Ok(Vegetables);
    }

    [HttpGet]
    [Route("vegetable")]
    public async Task<IActionResult> GetByQuieryAsync([FromQuery][Required] int id)
    {
        return Ok(Vegetables[id]);
    }

    [HttpDelete]
    [Route("vegetable/id/{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute][Required] int id)
    {
        Vegetables.RemoveAt(id);

        return Ok(Vegetables);
    }
}
