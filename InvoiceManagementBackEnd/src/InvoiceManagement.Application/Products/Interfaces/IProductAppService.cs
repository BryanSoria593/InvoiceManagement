using InvoiceManagement.Application.Common.Dtos;
using InvoiceManagement.Application.Products.Dtos;

namespace InvoiceManagement.Application.Products.Interfaces;
public interface IProductAppService
{
    PagedResultDto<ProductDto> GetProducts(int pageNumber, int pageSize);
    ProductDto? GetProductById(int id);
    ProductDto CreateProduct(CreateProductDto dto);
    ProductDto UpdateProduct(UpdateProductDto dto);
    void DeleteProduct(int id);
}
