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

    public ProductDto? GetProductById(int id)
    {
        var p = _productRepository.GetById(id);
        if (p == null || p.IsDeleted)
            return null;
        return new ProductDto
        {
            Id = p.Id,
            Code = p.Code,
            Name = p.Name,
            Description = p.Description,
            SalePrice = p.SalePrice,
            Status = p.Status,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt
        };
    }

    public ProductDto CreateProduct(CreateProductDto dto)
    {
        var product = new Domain.Products.Entities.Product
        {
            Code = dto.Code,
            Name = dto.Name,
            Description = dto.Description,
            SalePrice = dto.SalePrice,
            Status = dto.Status,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        _productRepository.Add(product);
        return new ProductDto
        {
            Id = product.Id,
            Code = product.Code,
            Name = product.Name,
            Description = product.Description,
            SalePrice = product.SalePrice,
            Status = product.Status,
            CreatedAt = product.CreatedAt
        };
    }

    public ProductDto UpdateProduct(UpdateProductDto dto)
    {
        var product = _productRepository.GetById(dto.Id);
        if (product == null || product.IsDeleted)
            throw new Exception("Producto no encontrado");
        product.Code = dto.Code;
        product.Name = dto.Name;
        product.Description = dto.Description;
        product.SalePrice = dto.SalePrice;
        product.Status = dto.Status;
        product.UpdatedAt = DateTime.UtcNow;
        _productRepository.Update(product);
        return new ProductDto
        {
            Id = product.Id,
            Code = product.Code,
            Name = product.Name,
            Description = product.Description,
            SalePrice = product.SalePrice,
            Status = product.Status,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };
    }

    public void DeleteProduct(int id)
    {
        var product = _productRepository.GetById(id);
        if (product == null || product.IsDeleted) return;
        product.IsDeleted = true;
        product.UpdatedAt = DateTime.UtcNow;
        _productRepository.Update(product);
    }
}
