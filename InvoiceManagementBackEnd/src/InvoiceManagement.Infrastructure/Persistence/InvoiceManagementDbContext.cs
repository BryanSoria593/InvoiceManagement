using Microsoft.EntityFrameworkCore;
using InvoiceManagement.Domain.Users.Entities;

namespace InvoiceManagement.Infrastructure.Persistence
{
    public class InvoiceManagementDbContext : DbContext
    {
        public InvoiceManagementDbContext(DbContextOptions<InvoiceManagementDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;

    }
}
