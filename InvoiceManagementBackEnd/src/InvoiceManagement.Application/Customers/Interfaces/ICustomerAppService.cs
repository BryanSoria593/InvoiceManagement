using System.Collections.Generic;
using InvoiceManagement.Application.Customers.Dtos;

namespace InvoiceManagement.Application.Customers.Interfaces;
public interface ICustomerAppService
{
    List<CustomerDto> GetAllCustomers();
    CustomerDto? GetCustomerById(int id);
    CustomerDto CreateCustomer(CreateCustomerDto dto);
    CustomerDto UpdateCustomer(UpdateCustomerDto dto);
    void DeleteCustomer(int id);
}
