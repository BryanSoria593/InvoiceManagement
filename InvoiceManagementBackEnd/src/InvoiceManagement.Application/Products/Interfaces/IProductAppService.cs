using InvoiceManagement.Application.Products.Dtos;

namespace InvoiceManagement.Application.Products.Interfaces;
public interface IProductAppService
{
    List<ProductDto> GetAllProducts();
    ProductDto? GetProductById(int id);
    ProductDto CreateProduct(CreateProductDto dto);
    ProductDto UpdateProduct(UpdateProductDto dto);
    void DeleteProduct(int id);
}
