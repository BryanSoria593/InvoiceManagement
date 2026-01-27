using System.Collections.Generic;
using System.Linq;
using InvoiceManagement.Application.PaymentMethods.Dtos;
using InvoiceManagement.Application.PaymentMethods.Interfaces;
using InvoiceManagement.Domain.PaymentMethods.Entities;
using InvoiceManagement.Domain.PaymentMethods.Interfaces;

namespace InvoiceManagement.Application.PaymentMethods.Services;
public class PaymentMethodAppService : IPaymentMethodAppService
{
    private readonly IPaymentMethodRepository _paymentMethodRepository;

    public PaymentMethodAppService(IPaymentMethodRepository paymentMethodRepository)
    {
        _paymentMethodRepository = paymentMethodRepository;
    }

    public List<PaymentMethodDto> GetAllPaymentMethods()
    {
        var methods = _paymentMethodRepository.GetAll();
        return methods.Select(m => new PaymentMethodDto
        {
            Id = m.Id,
            Name = m.Name,
            Status = m.Status,
            CreatedAt = m.CreatedAt,
            UpdatedAt = m.UpdatedAt
        }).ToList();
    }
}
