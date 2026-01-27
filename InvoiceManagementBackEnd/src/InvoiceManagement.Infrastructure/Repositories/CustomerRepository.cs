using System.Collections.Generic;
using InvoiceManagement.Domain.Customers.Entities;
using InvoiceManagement.Domain.Customers.Enums;
using InvoiceManagement.Domain.Customers.Interfaces;

namespace InvoiceManagement.Infrastructure.Repositories;
public class CustomerRepository : ICustomerRepository
{
    public List<Customer> GetAll()
    {
        return new List<Customer>
        {
            new Customer
            {
                Id = 1,
                Name = "John Doe",
                Phone = "1234567890",
                Email = "john@example.com",
                Address = "123 Main St",
                CreatedAt = DateTime.UtcNow.Date,
                UpdatedAt = DateTime.UtcNow.Date,
                Status = CustomerStatus.Active,
                IsDeleted = false
            },
            new Customer
            {
                Id = 2,
                Name = "Jane Smith",
                Phone = "0987654321",
                Email = "jane@example.com",
                Address = "456 Elm St",
                CreatedAt = DateTime.UtcNow.Date,
                Status = CustomerStatus.Inactive,
                IsDeleted = false
            }
        };
    }
}
