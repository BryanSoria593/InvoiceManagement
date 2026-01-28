using Microsoft.AspNetCore.Mvc;
using InvoiceManagement.Application.Invoices.Interfaces;
using InvoiceManagement.Application.Invoices.Dtos;
using System.Collections.Generic;

namespace InvoiceManagement.API.Controllers;

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
    public ActionResult<List<InvoiceDto>> Get()
    {
        var invoices = _invoiceAppService.GetAllInvoices();
        return Ok(invoices);
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
