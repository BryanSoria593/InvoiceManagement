using System.ComponentModel.DataAnnotations.Schema;
using InvoiceManagement.Domain.Products.Enums;

namespace InvoiceManagement.Domain.Products.Entities;
public class Product
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [Column(TypeName = "decimal(18,2)")]
    public decimal SalePrice { get; set; }
    public ProductStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}
