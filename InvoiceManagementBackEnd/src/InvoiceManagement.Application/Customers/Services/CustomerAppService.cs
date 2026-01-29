using System.Collections.Generic;
using System.Linq;
using InvoiceManagement.Application.Common.Dtos;
using InvoiceManagement.Application.Customers.Dtos;
using InvoiceManagement.Application.Customers.Interfaces;
using InvoiceManagement.Domain.Customers.Entities;
using InvoiceManagement.Domain.Customers.Enums;
using InvoiceManagement.Domain.Customers.Interfaces;

namespace InvoiceManagement.Application.Customers.Services;

public class CustomerAppService : ICustomerAppService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerAppService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public PagedResultDto<CustomerDto> GetCustomers(int pageNumber, int pageSize, string? filter = null)
    {
        var customers = _customerRepository.GetAll(pageNumber, pageSize, filter);
        var total = _customerRepository.GetTotalCount(filter);
        var items = customers.Select(c => new CustomerDto
        {
            Id = c.Id,
            Name = c.Name,
            Phone = c.Phone,
            Email = c.Email,
            Address = c.Address,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt,
            Status = c.Status,
            IsDeleted = c.IsDeleted
        }).ToList();
        return new PagedResultDto<CustomerDto>(items, total);
    }

    public CustomerDto? GetCustomerById(int id)
    {
        var c = _customerRepository.GetById(id);
        if (c == null || c.IsDeleted)
            return null;
        return new CustomerDto
        {
            Id = c.Id,
            Name = c.Name,
            Phone = c.Phone,
            Email = c.Email,
            Address = c.Address,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt,
            Status = c.Status,
            IsDeleted = c.IsDeleted
        };
    }

    public CustomerDto CreateCustomer(CreateCustomerDto dto)
    {
        var customer = new Customer
        {
            Name = dto.Name,
            Phone = dto.Phone,
            Email = dto.Email,
            Address = dto.Address,
            CreatedAt = DateTime.UtcNow,
            Status = CustomerStatus.Active,
            IsDeleted = false
        };
        _customerRepository.Add(customer);
        return new CustomerDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Phone = customer.Phone,
            Email = customer.Email,
            Address = customer.Address,
            CreatedAt = customer.CreatedAt,
            Status = customer.Status,
            IsDeleted = customer.IsDeleted
        };
    }

    public CustomerDto UpdateCustomer(UpdateCustomerDto dto)
    {
        var customer = _customerRepository.GetById(dto.Id);
        if (customer == null || customer.IsDeleted)
            throw new Exception("Cliente no encontrado");
        customer.Name = dto.Name;
        customer.Phone = dto.Phone;
        customer.Email = dto.Email;
        customer.Address = dto.Address;
        customer.UpdatedAt = DateTime.UtcNow;
        customer.Status = dto.Status;
        _customerRepository.Update(customer);
        return new CustomerDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Phone = customer.Phone,
            Email = customer.Email,
            Address = customer.Address,
            CreatedAt = customer.CreatedAt,
            UpdatedAt = customer.UpdatedAt,
            Status = customer.Status,
            IsDeleted = customer.IsDeleted
        };
    }

    public void DeleteCustomer(int id)
    {
        var customer = _customerRepository.GetById(id);
        if (customer == null || customer.IsDeleted) return;
        customer.IsDeleted = true;
        customer.UpdatedAt = DateTime.UtcNow;
        _customerRepository.Update(customer);
    }
}
