using InvoiceManagement.Domain.Products.Entities;
using InvoiceManagement.Domain.Products.Enums;
using InvoiceManagement.Domain.Products.Interfaces;

namespace InvoiceManagement.Infrastructure.Repositories;
using InvoiceManagement.Infrastructure.Persistence;

public class ProductRepository : IProductRepository
{
    private readonly InvoiceManagementDbContext _context;

    public ProductRepository(InvoiceManagementDbContext context)
    {
        _context = context;
    }

    public List<Product> GetAll()
    {
        return _context.Products.ToList();
    }

    public Product? GetById(int id)
    {
        return _context.Products.FirstOrDefault(p => p.Id == id);
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public void Update(Product product)
    {
        product.UpdatedAt = DateTime.UtcNow;
        _context.Products.Update(product);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            product.IsDeleted = true;
            product.UpdatedAt = DateTime.UtcNow;
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}
