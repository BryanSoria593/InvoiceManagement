using System;
using System.Collections.Generic;

namespace InvoiceManagement.Application.Invoices.Dtos;
public class UpdateInvoiceDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int UserId { get; set; }
    public DateTime Date { get; set; }
    public int PaymentMethodId { get; set; }
    public string? Observations { get; set; }
    public List<UpdateInvoiceDetailDto> Details { get; set; } = new();
}

public class UpdateInvoiceDetailDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string? Description { get; set; }
}
