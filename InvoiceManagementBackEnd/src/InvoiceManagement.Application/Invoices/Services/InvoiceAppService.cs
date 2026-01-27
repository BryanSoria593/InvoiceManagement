using System.Collections.Generic;
using System.Linq;
using InvoiceManagement.Application.Invoices.Dtos;
using InvoiceManagement.Application.Invoices.Interfaces;
using InvoiceManagement.Domain.Invoices.Entities;
using InvoiceManagement.Domain.Invoices.Interfaces;

namespace InvoiceManagement.Application.Invoices.Services;
public class InvoiceAppService : IInvoiceAppService
{
    private readonly IInvoiceRepository _invoiceRepository;

    public InvoiceAppService(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public List<InvoiceDto> GetAllInvoices()
    {
        var invoices = _invoiceRepository.GetAll();
        return invoices.Select(i => new InvoiceDto
        {
            Id = i.Id,
            CustomerId = i.CustomerId,
            UserId = i.UserId,
            Date = i.Date,
            PaymentMethodId = i.PaymentMethodId,
            Status = i.Status,
            Total = i.Total,
            Observations = i.Observations,
            IsDeleted = i.IsDeleted,
            CreatedAt = i.CreatedAt,
            UpdatedAt = i.UpdatedAt
        }).ToList();
    }
}
