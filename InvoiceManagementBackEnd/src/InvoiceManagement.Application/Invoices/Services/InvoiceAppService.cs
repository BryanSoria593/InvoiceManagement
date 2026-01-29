using System.Collections.Generic;
using System.Linq;
using InvoiceManagement.Application.Common.Dtos;
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
        return invoices.Select(MapToDto).ToList();
    }

    public PagedResultDto<InvoiceDto> GetInvoices(int pageNumber, int pageSize, string? filter = null)
    {
        var invoices = _invoiceRepository.GetAll(pageNumber, pageSize, filter);
        var total = _invoiceRepository.GetTotalCount(filter);
        var items = invoices.Select(MapToDto).ToList();
        return new PagedResultDto<InvoiceDto>(items, total);
    }

    public InvoiceDto? GetInvoiceById(int id)
    {
        var invoice = _invoiceRepository.GetById(id);
        return invoice == null ? null : MapToDto(invoice);
    }

    public InvoiceDto CreateInvoice(CreateInvoiceDto dto)
    {
        var invoice = new Invoice
        {
            CustomerId = dto.CustomerId,
            UserId = dto.UserId,
            Date = dto.Date,
            PaymentMethodId = dto.PaymentMethodId,
            Status = dto.Status,
            Total = dto.Total,
            Observations = dto.Observations,
            IsDeleted = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            InvoiceDetails = dto.Details.Select(d => new InvoiceDetail
            {
                ProductId = d.ProductId,
                Quantity = d.Quantity,
                UnitPrice = d.UnitPrice,
                Total = d.Total,
                Description = d.Description,
                IsDeleted = false
            }).ToList()
        };
        _invoiceRepository.Add(invoice);
        return MapToDto(invoice);
    }

    public InvoiceDto UpdateInvoice(UpdateInvoiceDto dto)
    {
        var invoice = _invoiceRepository.GetById(dto.Id);
        if (invoice == null) throw new Exception($"Invoice with Id {dto.Id} not found");
        invoice.CustomerId = dto.CustomerId;
        invoice.UserId = dto.UserId;
        invoice.Status = dto.Status;
        invoice.Date = dto.Date;
        invoice.PaymentMethodId = dto.PaymentMethodId;
        invoice.Observations = dto.Observations;
        invoice.UpdatedAt = DateTime.UtcNow;

        var dtoDetailIds = dto.Details.Select(d => d.Id).ToHashSet();
        foreach (var detail in invoice.InvoiceDetails.ToList())
        {
            if (!dtoDetailIds.Contains(detail.Id))
            {
                detail.IsDeleted = true;
            }
        }

        foreach (var dtoDetail in dto.Details)
        {
            var existingDetail = invoice.InvoiceDetails.FirstOrDefault(d => d.Id == dtoDetail.Id);
            if (existingDetail != null)
            {
                existingDetail.ProductId = dtoDetail.ProductId;
                existingDetail.Quantity = dtoDetail.Quantity;
                existingDetail.UnitPrice = dtoDetail.UnitPrice;
                existingDetail.Total = dtoDetail.Quantity * dtoDetail.UnitPrice;
                existingDetail.Description = dtoDetail.Description;
                existingDetail.IsDeleted = false;
            }
            else
            {
                invoice.InvoiceDetails.Add(new InvoiceDetail
                {
                    ProductId = dtoDetail.ProductId,
                    Quantity = dtoDetail.Quantity,
                    UnitPrice = dtoDetail.UnitPrice,
                    Total = dtoDetail.Quantity * dtoDetail.UnitPrice,
                    Description = dtoDetail.Description,
                    InvoiceId = invoice.Id,
                    IsDeleted = false
                });
            }
        }

        invoice.Total = invoice.InvoiceDetails.Where(x => !x.IsDeleted).Sum(x => x.Total);
        _invoiceRepository.Update(invoice);
        return MapToDto(invoice);
    }

    public void DeleteInvoice(int id)
    {
        var invoice = _invoiceRepository.GetById(id);
        if (invoice == null) return;
        invoice.IsDeleted = true;
        if (invoice.InvoiceDetails != null)
        {
            foreach (var detail in invoice.InvoiceDetails)
            {
                detail.IsDeleted = true;
            }
        }
        _invoiceRepository.Update(invoice);
    }

    private InvoiceDto MapToDto(Invoice i)
    {
        return new InvoiceDto
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
            UpdatedAt = i.UpdatedAt ?? null,
            CustomerName = i.Customer?.Name ?? string.Empty,
            UserName = i.User?.FirstName ?? string.Empty,
            PaymentMethodName = i.PaymentMethod?.Name ?? string.Empty,
            CustomerPhone = i.Customer?.Phone ?? string.Empty,
            CustomerEmail = i.Customer?.Email ?? string.Empty,
            Details = i.InvoiceDetails?.Where(d => !d.IsDeleted).Select(d => new InvoiceDetailDto
            {
                Id = d.Id,
                ProductId = d.ProductId,
                ProductCode = d.Product?.Code ?? string.Empty,
                Quantity = d.Quantity,
                UnitPrice = d.UnitPrice,
                Total = d.Total,
                Description = d.Description
            }).ToList() ?? new List<InvoiceDetailDto>()
        };
    }
}
