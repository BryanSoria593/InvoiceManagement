using Microsoft.AspNetCore.Mvc;
using InvoiceManagement.Application.Customers.Interfaces;
using InvoiceManagement.Application.Customers.Dtos;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

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

    [HttpGet]
    public ActionResult<List<CustomerDto>> Get()
    {
        var customers = _customerAppService.GetAllCustomers();
        return Ok(customers);
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
