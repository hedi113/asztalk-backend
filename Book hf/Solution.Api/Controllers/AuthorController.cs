namespace Solution.Api.Controllers;

public class AuthorController(IAuthorService authorService) : BaseController
{
    [HttpGet]
    [Route("api/author/all")]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await authorService.GetAllAsync();

        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    [HttpGet]
    [Route("api/author/page/{page}")]
    public async Task<IActionResult> GetPagedAsync([FromRoute] int page = 0)
    {
        var result = await authorService.GetPagedAsync(page);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    [HttpGet]
    [Route("api/author/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute][Required] int id)
    {
        var result = await authorService.GetByIdAsync(id);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    [HttpDelete]
    [Route("api/author/delete/{id}")]
    public async Task<IActionResult> DeleteById([FromRoute][Required] int id)
    {
        var result = await authorService.DeleteAsync(id);

        return result.Match(
            result => Ok(new OkResult()),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    [Route("api/author/create")]
    public async Task<IActionResult> CreateAsync([FromBody][Required] AuthorModel model)
    {
        var result = await authorService.CreateAsync(model);

        return result.Match(
            result => Ok(result), 
            errors => Problem(errors)    
        );
    }

    [HttpPut]
    [Route("api/author/update")]
    public async Task<IActionResult> UpdateAsync([FromBody][Required]AuthorModel model)
    {
        var result = await authorService.UpdateAsync(model);

        return result.Match(
            result => Ok(new OkResult()),
            errors => Problem(errors)
        );
    }
}
