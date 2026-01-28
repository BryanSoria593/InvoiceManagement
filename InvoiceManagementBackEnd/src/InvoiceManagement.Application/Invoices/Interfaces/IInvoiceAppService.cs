using System.Collections.Generic;
using InvoiceManagement.Application.Invoices.Dtos;

namespace InvoiceManagement.Application.Invoices.Interfaces;
public interface IInvoiceAppService
{
    List<InvoiceDto> GetAllInvoices();
    InvoiceDto? GetInvoiceById(int id);
    InvoiceDto CreateInvoice(CreateInvoiceDto dto);
    InvoiceDto UpdateInvoice(UpdateInvoiceDto dto);
    void DeleteInvoice(int id);
}
