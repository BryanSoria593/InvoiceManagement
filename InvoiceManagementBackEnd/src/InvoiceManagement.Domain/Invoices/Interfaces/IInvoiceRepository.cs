using System.Collections.Generic;
using InvoiceManagement.Domain.Invoices.Entities;

namespace InvoiceManagement.Domain.Invoices.Interfaces;
public interface IInvoiceRepository
{
    List<Invoice> GetAll();
}
