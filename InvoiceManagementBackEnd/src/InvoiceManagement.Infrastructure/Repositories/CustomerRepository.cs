using System.Collections.Generic;
using InvoiceManagement.Domain.Customers.Entities;
using InvoiceManagement.Domain.Customers.Enums;
using InvoiceManagement.Domain.Customers.Interfaces;

namespace InvoiceManagement.Infrastructure.Repositories;
using InvoiceManagement.Infrastructure.Persistence;

public class CustomerRepository : ICustomerRepository
{
    private readonly InvoiceManagementDbContext _context;

    public CustomerRepository(InvoiceManagementDbContext context)
    {
        _context = context;
    }

    public List<Customer> GetAll()
    {
        return _context.Customers.ToList();
    }

    public Customer? GetById(int id)
    {
        return _context.Customers.FirstOrDefault(c => c.Id == id);
    }

    public void Add(Customer customer)
    {
        _context.Customers.Add(customer);
        _context.SaveChanges();
    }

    public void Update(Customer customer)
    {
        customer.UpdatedAt = DateTime.UtcNow;
        _context.Customers.Update(customer);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var customer = _context.Customers.FirstOrDefault(c => c.Id == id);
        if (customer != null)
        {
            customer.IsDeleted = true;
            customer.UpdatedAt = DateTime.UtcNow;
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }
    }
}
