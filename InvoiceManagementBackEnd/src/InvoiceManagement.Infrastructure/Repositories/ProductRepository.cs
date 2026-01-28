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

    public List<Product> GetAll(int pageNumber, int pageSize)
    {
        return _context.Products
            .Where(p => !p.IsDeleted)
            .OrderBy(p => p.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public int GetTotalCount()
    {
        return _context.Products.Count(p => !p.IsDeleted);
    }

    public Product? GetById(int id)
    {
        return _context.Products.FirstOrDefault(p => p.Id == id && !p.IsDeleted);
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
        _context.SaveChanges();
    }

}
