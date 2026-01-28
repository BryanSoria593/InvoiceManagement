using System;

namespace InvoiceManagement.Application.Invoices.Dtos;

public class CreateInvoiceDetailDto
{
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string? Description { get; set; }
}