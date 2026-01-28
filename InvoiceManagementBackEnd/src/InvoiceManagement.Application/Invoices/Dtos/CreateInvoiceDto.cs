using System;
using System.Collections.Generic;

namespace InvoiceManagement.Application.Invoices.Dtos;
public class CreateInvoiceDto
{
    public int CustomerId { get; set; }
    public int UserId { get; set; }
    public DateTime Date { get; set; }
    public int PaymentMethodId { get; set; }
    public string? Observations { get; set; }
    public List<CreateInvoiceDetailDto> Details { get; set; } = new();
}
