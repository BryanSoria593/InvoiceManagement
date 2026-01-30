
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace InvoiceManagement.Infrastructure.Persistence
{
    public class InvoiceManagementDbContextFactory : IDesignTimeDbContextFactory<InvoiceManagementDbContext>
    {
        public InvoiceManagementDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<InvoiceManagementDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new InvoiceManagementDbContext(optionsBuilder.Options);
        }
    }
}
