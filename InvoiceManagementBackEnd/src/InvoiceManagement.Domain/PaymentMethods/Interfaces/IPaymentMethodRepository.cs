using System.Collections.Generic;
using InvoiceManagement.Domain.PaymentMethods.Entities;

namespace InvoiceManagement.Domain.PaymentMethods.Interfaces;
public interface IPaymentMethodRepository
{
    List<PaymentMethod> GetAll();
}
