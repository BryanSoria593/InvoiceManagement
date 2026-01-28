using InvoiceManagement.Domain.PaymentMethods.Entities;
using InvoiceManagement.Domain.PaymentMethods.Interfaces;
using InvoiceManagement.Infrastructure.Persistence;

namespace InvoiceManagement.Infrastructure.Repositories;
public class PaymentMethodRepository : IPaymentMethodRepository
{
    private readonly InvoiceManagementDbContext _context;

    public PaymentMethodRepository(InvoiceManagementDbContext context)
    {
        _context = context;
    }

    public List<PaymentMethod> GetAll()
    {
        return _context.PaymentMethods.ToList();
    }
}
