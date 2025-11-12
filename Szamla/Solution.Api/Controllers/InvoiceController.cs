using Solution.Services;

namespace Solution.Api.Controllers;

public class InvoiceController(IInvoiceService invoiceService) : BaseController
{
    [HttpGet]
    [Route("api/invoices/all")]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await invoiceService.GetAllAsync();

        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    [HttpGet]
    [Route("api/invoices/page/{page}")]
    public async Task<IActionResult> GetPagedAsync([FromRoute] int page = 0)
    {
        var result = await invoiceService.GetPagedAsync(page);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    [HttpGet]
    [Route("api/invoices/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute][Required] int id)
    {
        var result = await invoiceService.GetByIdAsync(id);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    [HttpDelete]
    [Route("api/invoices/delete/{id}")]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute][Required] int id)
    {
        var result = await invoiceService.DeleteAsync(id);

        return result.Match(
            result => Ok(new OkResult()),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    [Route("api/invoices/create")]
    public async Task<IActionResult> CreateAsync([FromBody][Required] InvoiceModel model)
    {
        var result = await invoiceService.CreateAsync(model);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }

    [HttpPut]
    [Route("api/invoices/update")]
    public async Task<IActionResult> UpdateAsync([FromBody][Required] InvoiceModel model)
    {
        var result = await invoiceService.UpdateAsync(model);

        return result.Match(
            result => Ok(new OkResult()),
            errors => Problem(errors)
        );
    }
}
