using InvoiceManagement.Domain.Configuration.Entities;
using InvoiceManagement.Domain.Configuration.Interfaces;

namespace InvoiceManagement.Infrastructure.Repositories;
public class ConfigurationRepository : IConfigurationRepository
{
    public Configuration? Get()
    {
        return new Configuration
        {
            Id = 1,
            CompanyName = "Invoice Management Corp.",
            Phone = "+(503) 2682-5550",
            Email = "zansoriam@gmail.com",
            Address = "Guayas, Guayaquil, Ecuador",
            City = "Guayaquil",
            Region = "Portete",
            PostalCode = "3301",
            VatPercentage = 13.00m,
            CurrencySymbol = "$",
            LogoUrl = "",
            UpdatedAt = DateTime.UtcNow
        };
    }
}
