using System.Collections.Generic;
using InvoiceManagement.Application.Invoices.Dtos;

namespace InvoiceManagement.Application.Invoices.Interfaces;
public interface IInvoiceAppService
{
    List<InvoiceDto> GetAllInvoices();
}
