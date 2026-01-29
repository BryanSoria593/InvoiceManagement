using System.Collections.Generic;
using InvoiceManagement.Application.Common.Dtos;
using InvoiceManagement.Application.Invoices.Dtos;

namespace InvoiceManagement.Application.Invoices.Interfaces;
public interface IInvoiceAppService
{
    List<InvoiceDto> GetAllInvoices();
    PagedResultDto<InvoiceDto> GetInvoices(int pageNumber, int pageSize, string? filter = null);
    InvoiceDto? GetInvoiceById(int id);
    InvoiceDto CreateInvoice(CreateInvoiceDto dto);
    InvoiceDto UpdateInvoice(UpdateInvoiceDto dto);
    void DeleteInvoice(int id);
}
