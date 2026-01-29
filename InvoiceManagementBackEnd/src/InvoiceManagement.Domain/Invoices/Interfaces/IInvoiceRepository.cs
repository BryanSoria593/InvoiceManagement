using System.Collections.Generic;
using InvoiceManagement.Domain.Invoices.Entities;

namespace InvoiceManagement.Domain.Invoices.Interfaces;
public interface IInvoiceRepository
{
    List<Invoice> GetAll();
    List<Invoice> GetAll(int pageNumber, int pageSize, string? filter = null);
    int GetTotalCount(string? filter = null);
    Invoice? GetById(int id);
    void Add(Invoice invoice);
    void Update(Invoice invoice);
}
