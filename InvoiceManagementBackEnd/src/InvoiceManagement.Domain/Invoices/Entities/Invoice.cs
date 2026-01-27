using System.ComponentModel.DataAnnotations.Schema;
using InvoiceManagement.Domain.Customers.Entities;
using InvoiceManagement.Domain.Invoices.Enums;
using InvoiceManagement.Domain.PaymentMethods.Entities;
using InvoiceManagement.Domain.Users.Entities;

namespace InvoiceManagement.Domain.Invoices.Entities;
public class Invoice
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public InvoiceStatus Status { get; set; }
    public decimal Total { get; set; }
    public string? Observations { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }

    public int PaymentMethodId { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }

}
