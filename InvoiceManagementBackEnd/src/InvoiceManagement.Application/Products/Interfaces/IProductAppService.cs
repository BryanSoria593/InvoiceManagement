using InvoiceManagement.Application.Products.Dtos;

namespace InvoiceManagement.Application.Products.Interfaces;
public interface IProductAppService
{
    List<ProductDto> GetAllProducts();
}
