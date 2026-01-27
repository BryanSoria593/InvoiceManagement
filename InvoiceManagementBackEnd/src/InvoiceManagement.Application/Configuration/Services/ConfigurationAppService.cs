using InvoiceManagement.Application.Configuration.Dtos;
using InvoiceManagement.Application.Configuration.Interfaces;
using InvoiceManagement.Domain.Configuration.Interfaces;

namespace InvoiceManagement.Application.Configuration.Services;
public class ConfigurationAppService : IConfigurationAppService
{
    private readonly IConfigurationRepository _configurationRepository;

    public ConfigurationAppService(IConfigurationRepository configurationRepository)
    {
        _configurationRepository = configurationRepository;
    }

    public ConfigurationDto? GetConfiguration()
    {
        var config = _configurationRepository.Get();
        if (config == null) return null;
        return new ConfigurationDto
        {
            Id = config.Id,
            CompanyName = config.CompanyName,
            Phone = config.Phone,
            Email = config.Email,
            Address = config.Address,
            City = config.City,
            Region = config.Region,
            PostalCode = config.PostalCode,
            VatPercentage = config.VatPercentage,
            CurrencySymbol = config.CurrencySymbol,
            LogoUrl = config.LogoUrl,
            UpdatedAt = config.UpdatedAt
        };
    }
}
