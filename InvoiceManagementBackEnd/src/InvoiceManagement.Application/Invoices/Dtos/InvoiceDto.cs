using InvoiceManagement.Domain.Invoices.Enums;

namespace InvoiceManagement.Application.Invoices.Dtos;
public class InvoiceDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int PaymentMethodId { get; set; }
    public InvoiceStatus Status { get; set; }
    public decimal Total { get; set; }
    public string? Observations { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public List<InvoiceDetailDto>? Details { get; set; }
}
