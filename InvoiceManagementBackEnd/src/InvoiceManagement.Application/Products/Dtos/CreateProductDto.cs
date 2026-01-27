using InvoiceManagement.Domain.Products.Enums;

namespace InvoiceManagement.Application.Products.Dtos;
public class CreateProductDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal SalePrice { get; set; }
    public ProductStatus Status { get; set; }
}
