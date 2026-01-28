using System.Collections.Generic;
using InvoiceManagement.Domain.Invoices.Entities;

namespace InvoiceManagement.Domain.Invoices.Interfaces;
public interface IInvoiceRepository
{
    List<Invoice> GetAll();
    Invoice? GetById(int id);
    void Add(Invoice invoice);
    void Update(Invoice invoice);
}
