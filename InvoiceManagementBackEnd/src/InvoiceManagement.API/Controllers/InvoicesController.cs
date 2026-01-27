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
}
