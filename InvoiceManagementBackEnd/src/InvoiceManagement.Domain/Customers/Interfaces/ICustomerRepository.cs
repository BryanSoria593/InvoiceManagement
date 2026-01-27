using System.Collections.Generic;
using InvoiceManagement.Domain.Customers.Entities;

namespace InvoiceManagement.Domain.Customers.Interfaces;
public interface ICustomerRepository
{
    List<Customer> GetAll();
    Customer? GetById(int id);
    void Add(Customer customer);
    void Update(Customer customer);
    void Delete(int id);
}
