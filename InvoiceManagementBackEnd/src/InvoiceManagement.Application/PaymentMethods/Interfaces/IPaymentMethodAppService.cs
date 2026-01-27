using System.Collections.Generic;
using InvoiceManagement.Application.PaymentMethods.Dtos;

namespace InvoiceManagement.Application.PaymentMethods.Interfaces;
public interface IPaymentMethodAppService
{
    List<PaymentMethodDto> GetAllPaymentMethods();
}
