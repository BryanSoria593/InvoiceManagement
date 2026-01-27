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
                UpdatedAt = DateTime.UtcNow,
                InvoiceDetails = new List<InvoiceDetail>
                {
                    new InvoiceDetail
                    {
                        Id = 1,
                        InvoiceId = 1,
                        ProductId = 1,
                        Quantity = 2,
                        UnitPrice = 50.00m,
                        Total = 100.00m,
                        Description = "Producto de ejemplo"
                    }
                }
            }
        };
    }
}
