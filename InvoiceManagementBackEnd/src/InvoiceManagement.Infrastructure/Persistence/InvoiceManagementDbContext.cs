using Microsoft.EntityFrameworkCore;
using InvoiceManagement.Domain.Users.Entities;
using InvoiceManagement.Domain.Products.Entities;
using InvoiceManagement.Domain.Customers.Entities;
using InvoiceManagement.Domain.PaymentMethods.Entities;

namespace InvoiceManagement.Infrastructure.Persistence;
public class InvoiceManagementDbContext : DbContext
{
    public InvoiceManagementDbContext(DbContextOptions<InvoiceManagementDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
}
