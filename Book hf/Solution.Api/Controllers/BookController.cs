


namespace Solution.Api.Controllers;

public class BookController(IBookService bookService) : BaseController
{
    [HttpGet]
    [Route("api/book/all")]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await bookService.GetAllAsync();

        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    [HttpGet]
    [Route("api/book/{page}")]
    public async Task<IActionResult> GetPagedAsync([FromRoute] int page = 0)
    {
        var result = await bookService.GetPagedAsync(page);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    [HttpGet]
    [Route("api/book/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute][Required] string id)
    {
        var result = await bookService.GetByIdAsync(id);

        return result.Match(
            result => Ok(new OkResult()),
            errors => Problem(errors)
        );
    }

    [HttpDelete]
    [Route("api/book/delete/{id}")]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute][Required] string id)
    {
        var result = await bookService.DeleteAsync(id);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    [Route("api/book/create")]
    public async Task<IActionResult> CreateAsync([FromBody][Required] BookModel model)
    {
        var result = await bookService.CreateAsync(model);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    [HttpPut]
    [Route("api/book/update")]
    public async Task<IActionResult> UpdateAsync([FromBody][Required] BookModel model)
    {
        var result = await bookService.UpdateAsync(model);

        return result.Match(
            result => Ok(new OkResult()),
            errors => Problem(errors)
        );
    }

}
