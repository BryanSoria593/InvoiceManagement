using InvoiceManagement.Domain.Products.Entities;
using InvoiceManagement.Domain.Products.Enums;
using InvoiceManagement.Domain.Products.Interfaces;

namespace InvoiceManagement.Infrastructure.Repositories;
public class ProductRepository : IProductRepository
{
    public List<Product> GetAll()
    {
        return new List<Product>
        {
            new Product
            {
                Id = 1,
                Code = "P001",
                Name = "Product 1",
                Description = "Description for Product 1",
                SalePrice = 100.00m,
                Status = ProductStatus.Active,
                CreatedAt = System.DateTime.UtcNow,
                UpdatedAt = System.DateTime.UtcNow
            },
            new Product
            {
                Id = 2,
                Code = "P002",
                Name = "Product 2",
                Description = "Description for Product 2",
                SalePrice = 200.00m,
                Status = ProductStatus.Inactive,
                CreatedAt = System.DateTime.UtcNow,
                UpdatedAt = System.DateTime.UtcNow
            }
        };
    }
}
