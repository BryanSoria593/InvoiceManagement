using InvoiceManagement.Domain.PaymentMethods.Entities;

namespace InvoiceManagement.Application.PaymentMethods.Dtos;

public class PaymentMethodDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public PaymentMethodStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
