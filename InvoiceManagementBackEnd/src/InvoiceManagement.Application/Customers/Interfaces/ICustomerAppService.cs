using System.Collections.Generic;
using InvoiceManagement.Application.Customers.Dtos;

namespace InvoiceManagement.Application.Customers.Interfaces;
public interface ICustomerAppService
{
    List<CustomerDto> GetAllCustomers();
}
