using InvoiceManagement.Domain.Configuration.Entities;
using InvoiceManagement.Domain.Configuration.Interfaces;

namespace InvoiceManagement.Infrastructure.Repositories;
using InvoiceManagement.Infrastructure.Persistence;

public class ConfigurationRepository : IConfigurationRepository
{
    private readonly InvoiceManagementDbContext _context;

    public ConfigurationRepository(InvoiceManagementDbContext context)
    {
        _context = context;
    }

    public List<Configuration> GetAll()
    {
        return _context.Configuration.ToList();
    }

    public void Update(Configuration configuration)
    {
        _context.Configuration.Update(configuration);
        _context.SaveChanges();
    }

}
