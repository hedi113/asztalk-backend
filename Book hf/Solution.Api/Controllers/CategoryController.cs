using Solution.Services;

namespace Solution.Api.Controllers;

public class CategoryController(ICategoryService categoryService) : BaseController
{
    [HttpGet]
    [Route("api/category/all")]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await categoryService.GetAllAsync();

        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    [HttpGet]
    [Route("api/category/page/{page}")]
    public async Task<IActionResult> GetPagedAsync([FromRoute] int page = 0)
    {
        var result = await categoryService.GetPagedAsync(page);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    [HttpGet]
    [Route("api/category/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id = 0)
    {
        var result = await categoryService.GetByIdAsync(id);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    [HttpDelete]
    [Route("api/category/delete/{id}")]
    public async Task<IActionResult> DeleteById([FromRoute][Required] int id)
    {
        var result = await categoryService.DeleteAsync(id);

        return result.Match(
            result => Ok(result), 
            errors => Problem(errors)
        );
    }

    [HttpPost]
    [Route("api/category/create")]
    public async Task<IActionResult> CreateAsync([FromBody][Required] CategoryModel model)
    {
        var result = await categoryService.CreateAsync(model);

        return result.Match(
            result => Ok(new OkResult()),
            errors => Problem(errors)
        );
    }

    [HttpPut]
    [Route("api/category/update")]
    public async Task<IActionResult> UpdateAsync([FromBody][Required] CategoryModel model)
    {
        var result = await categoryService.UpdateAsync(model);

        return result.Match(
            result => Ok(new OkResult()),
            errors => Problem(errors)
        );
    }
}
