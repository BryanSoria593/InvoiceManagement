using System.Collections.Generic;
using InvoiceManagement.Domain.Customers.Entities;

namespace InvoiceManagement.Domain.Customers.Interfaces;
public interface ICustomerRepository
{
    List<Customer> GetAll();
}
