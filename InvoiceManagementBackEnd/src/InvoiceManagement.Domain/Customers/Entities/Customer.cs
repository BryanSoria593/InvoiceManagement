using InvoiceManagement.Domain.Customers.Enums;

namespace InvoiceManagement.Domain.Customers.Entities;
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public CustomerStatus Status { get; set; }
    public bool IsDeleted { get; set; }
}
