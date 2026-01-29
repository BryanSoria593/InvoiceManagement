using Microsoft.AspNetCore.Mvc;
using InvoiceManagement.Application.Customers.Interfaces;
using InvoiceManagement.Application.Customers.Dtos;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using InvoiceManagement.Application.Common.Dtos;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerAppService _customerAppService;

    public CustomersController(ICustomerAppService customerAppService)
    {
        _customerAppService = customerAppService;
    }

    [HttpGet("all")]
    public ActionResult<List<CustomerDto>> GetAll()
    {
        var customers = _customerAppService.GetAllActiveCustomers();
        return Ok(customers);
    }

    [HttpGet]
    public ActionResult<PagedResultDto<CustomerDto>> Get(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? filter = null)
    {
        var result = _customerAppService.GetCustomers(pageNumber, pageSize, filter);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<CustomerDto> GetById(int id)
    {
        var customer = _customerAppService.GetCustomerById(id);
        if (customer == null)
            return NotFound();
        return Ok(customer);
    }

    [HttpPost]
    public ActionResult<CustomerDto> Create([FromBody] CreateCustomerDto dto)
    {
        var customer = _customerAppService.CreateCustomer(dto);
        return Ok(customer);
    }

    [HttpPut]
    public ActionResult<CustomerDto> Update([FromBody] UpdateCustomerDto dto)
    {
        var customer = _customerAppService.UpdateCustomer(dto);
        return Ok(customer);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _customerAppService.DeleteCustomer(id);
        return NoContent();
    }
}
