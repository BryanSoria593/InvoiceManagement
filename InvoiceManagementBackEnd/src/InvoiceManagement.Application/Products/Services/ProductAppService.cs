using InvoiceManagement.Application.Products.Dtos;
using InvoiceManagement.Application.Products.Interfaces;
using InvoiceManagement.Domain.Products.Interfaces;

namespace InvoiceManagement.Application.Products.Services;
public class ProductAppService : IProductAppService
{
    private readonly IProductRepository _productRepository;

    public ProductAppService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public List<ProductDto> GetAllProducts()
    {
        var products = _productRepository.GetAll();
        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Code = p.Code,
            Name = p.Name,
            Description = p.Description,
            SalePrice = p.SalePrice,
            Status = p.Status,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt
        }).ToList();
    }
}
