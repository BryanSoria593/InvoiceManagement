using InvoiceManagement.Domain.PaymentMethods.Entities;
using InvoiceManagement.Domain.PaymentMethods.Interfaces;

namespace InvoiceManagement.Infrastructure.Repositories;
public class PaymentMethodRepository : IPaymentMethodRepository
{
    public List<PaymentMethod> GetAll()
    {
        return new List<PaymentMethod>
        {
            new PaymentMethod { Id = 1, Name = "Efectivo", Status = PaymentMethodStatus.Active, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new PaymentMethod { Id = 2, Name = "Cheque", Status = PaymentMethodStatus.Active, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new PaymentMethod { Id = 3, Name = "Transferencia bancaria", Status = PaymentMethodStatus.Active, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new PaymentMethod { Id = 4, Name = "Crédito", Status = PaymentMethodStatus.Active, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        };
    }
}
