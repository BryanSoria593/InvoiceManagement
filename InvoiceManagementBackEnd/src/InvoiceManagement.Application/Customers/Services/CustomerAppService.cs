using System.Collections.Generic;
using System.Linq;
using InvoiceManagement.Application.Customers.Dtos;
using InvoiceManagement.Application.Customers.Interfaces;
using InvoiceManagement.Domain.Customers.Entities;
using InvoiceManagement.Domain.Customers.Interfaces;

namespace InvoiceManagement.Application.Customers.Services;
public class CustomerAppService : ICustomerAppService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerAppService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public List<CustomerDto> GetAllCustomers()
    {
        var customers = _customerRepository.GetAll();
        return customers.Select(c => new CustomerDto
        {
            Id = c.Id,
            Name = c.Name,
            Phone = c.Phone,
            Email = c.Email,
            Address = c.Address,
            CreatedAt = c.CreatedAt,
            Status = c.Status,
            IsDeleted = c.IsDeleted
        }).ToList();
    }
}
