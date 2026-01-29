using System.Collections.Generic;
using InvoiceManagement.Domain.Customers.Entities;

namespace InvoiceManagement.Domain.Customers.Interfaces;
public interface ICustomerRepository
{
    List<Customer> GetAll();
    List<Customer> GetAll(int pageNumber, int pageSize, string? filter = null);
    int GetTotalCount(string? filter = null);
    Customer? GetById(int id);
    void Add(Customer customer);
    void Update(Customer customer);
}
