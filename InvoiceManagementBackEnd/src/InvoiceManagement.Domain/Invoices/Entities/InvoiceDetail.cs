using System.ComponentModel.DataAnnotations.Schema;
using InvoiceManagement.Domain.Products.Entities;

namespace InvoiceManagement.Domain.Invoices.Entities;
public class InvoiceDetail
{
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    public int ProductId { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Quantity { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }
    public string? Description { get; set; }

    public Invoice? Invoice { get; set; }
    public Product? Product { get; set; }
}
