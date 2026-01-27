using System.Collections.Generic;
using InvoiceManagement.Domain.Invoices.Entities;
using InvoiceManagement.Domain.Invoices.Enums;
using InvoiceManagement.Domain.Invoices.Interfaces;

namespace InvoiceManagement.Infrastructure.Repositories;
public class InvoiceRepository : IInvoiceRepository
{
    public List<Invoice> GetAll()
    {
        return new List<Invoice>
        {
            new Invoice
            {
                Id = 1,
                CustomerId = 1,
                UserId = 1,
                Date = DateTime.UtcNow,
                PaymentMethodId = 1,
                Status = InvoiceStatus.Paid,
                Total = 100.00m,
                Observations = "",
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };
    }
}
