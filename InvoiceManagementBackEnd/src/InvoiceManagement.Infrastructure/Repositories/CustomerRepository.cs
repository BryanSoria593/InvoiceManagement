using System.Collections.Generic;
using System.Linq;
using InvoiceManagement.Domain.Customers.Entities;
using InvoiceManagement.Domain.Customers.Interfaces;
using InvoiceManagement.Infrastructure.Persistence;

namespace InvoiceManagement.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly InvoiceManagementDbContext _context;

    public CustomerRepository(InvoiceManagementDbContext context)
    {
        _context = context;
    }

    public List<Customer> GetAll(int pageNumber, int pageSize, string? filter = null)
    {
        var query = _context.Customers
            .Where(c => !c.IsDeleted);

        if (!string.IsNullOrWhiteSpace(filter))
        {
            query = query.Where(c =>
                c.Name.Contains(filter) ||
                c.Phone.Contains(filter) ||
                (c.Email != null && c.Email.Contains(filter)) ||
                (c.Address != null && c.Address.Contains(filter))
            );
        }

        return query
            .OrderBy(c => c.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public int GetTotalCount(string? filter = null)
    {
        var query = _context.Customers
            .Where(c => !c.IsDeleted);

        if (!string.IsNullOrWhiteSpace(filter))
        {
            query = query.Where(c =>
                c.Name.Contains(filter) ||
                c.Phone.Contains(filter) ||
                (c.Email != null && c.Email.Contains(filter)) ||
                (c.Address != null && c.Address.Contains(filter))
            );
        }

        return query.Count();
    }

    public List<Customer> GetAll()
    {
        return _context.Customers.Where(c => !c.IsDeleted).ToList();
    }

    public Customer? GetById(int id)
    {
        return _context.Customers.FirstOrDefault(c => c.Id == id && !c.IsDeleted);
    }

    public void Add(Customer customer)
    {
        _context.Customers.Add(customer);
        _context.SaveChanges();
    }

    public void Update(Customer customer)
    {
        _context.Customers.Update(customer);
        _context.SaveChanges();
    }

}
