using InvoiceManagement.Domain.Products.Entities;

namespace InvoiceManagement.Domain.Products.Interfaces;
public interface IProductRepository
{
    List<Product> GetAll();
    Product? GetById(int id);
    void Add(Product product);
    void Update(Product product);
}
