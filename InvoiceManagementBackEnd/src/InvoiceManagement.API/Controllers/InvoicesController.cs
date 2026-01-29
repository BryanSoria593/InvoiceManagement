using Microsoft.AspNetCore.Mvc;
using InvoiceManagement.Application.Invoices.Interfaces;
using InvoiceManagement.Application.Invoices.Dtos;
using System.Collections.Generic;
using InvoiceManagement.Application.Common.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace InvoiceManagement.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly IInvoiceAppService _invoiceAppService;

    public InvoicesController(IInvoiceAppService invoiceAppService)
    {
        _invoiceAppService = invoiceAppService;
    }

    [HttpGet]
    public ActionResult<PagedResultDto<InvoiceDto>> Get(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? filter = null)
    {
        var result = _invoiceAppService.GetInvoices(pageNumber, pageSize, filter);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<InvoiceDto> GetById(int id)
    {
        var invoice = _invoiceAppService.GetInvoiceById(id);
        if (invoice == null) return NotFound();
        return Ok(invoice);
    }

    [HttpPost]
    public ActionResult<InvoiceDto> Create([FromBody] CreateInvoiceDto dto)
    {
        var invoice = _invoiceAppService.CreateInvoice(dto);
        return CreatedAtAction(nameof(GetById), new { id = invoice.Id }, invoice);
    }

    [HttpPut]
    public ActionResult<InvoiceDto> Update([FromBody] UpdateInvoiceDto dto)
    {
        var invoice = _invoiceAppService.UpdateInvoice(dto);
        return Ok(invoice);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _invoiceAppService.DeleteInvoice(id);
        return NoContent();
    }
}
