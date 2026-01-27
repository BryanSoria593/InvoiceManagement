using Microsoft.AspNetCore.Mvc;
using InvoiceManagement.Application.Customers.Interfaces;
using InvoiceManagement.Application.Customers.Dtos;
using System.Collections.Generic;

namespace InvoiceManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerAppService _customerAppService;

    public CustomersController(ICustomerAppService customerAppService)
    {
        _customerAppService = customerAppService;
    }

    [HttpGet]
    public ActionResult<List<CustomerDto>> Get()
    {
        var customers = _customerAppService.GetAllCustomers();
        return Ok(customers);
    }
}
