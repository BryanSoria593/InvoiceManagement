using System.Collections.Generic;
using InvoiceManagement.Application.Common.Dtos;
using InvoiceManagement.Application.Customers.Dtos;

namespace InvoiceManagement.Application.Customers.Interfaces;
public interface ICustomerAppService
{
    List<CustomerDto> GetAllActiveCustomers();
    PagedResultDto<CustomerDto> GetCustomers(int pageNumber, int pageSize, string? filter = null);
    CustomerDto? GetCustomerById(int id);
    CustomerDto CreateCustomer(CreateCustomerDto dto);
    CustomerDto UpdateCustomer(UpdateCustomerDto dto);
    void DeleteCustomer(int id);
}
