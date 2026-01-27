using InvoiceManagement.Domain.Products.Entities;

namespace InvoiceManagement.Domain.Products.Interfaces;
public interface IProductRepository
{
    List<Product> GetAll();
}
