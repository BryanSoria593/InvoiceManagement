namespace InvoiceManagement.Application.Configuration.Services;
using InvoiceManagement.Application.Configuration.Dtos;
using InvoiceManagement.Application.Configuration.Interfaces;
using InvoiceManagement.Domain.Configuration.Interfaces;

public class ConfigurationAppService : IConfigurationAppService
{
    private readonly IConfigurationRepository _configurationRepository;

    public ConfigurationAppService(IConfigurationRepository configurationRepository)
    {
        _configurationRepository = configurationRepository;
    }


    public List<ConfigurationDto> GetAllConfigurations()
    {
        return _configurationRepository.GetAll().Select(config => new ConfigurationDto
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
        }).ToList();
    }

    public ConfigurationDto UpdateConfiguration(UpdateConfigurationDto dto)
    {
        var config = _configurationRepository.GetAll().FirstOrDefault();
        if (config == null) throw new Exception("No configuration found to update");
        config.CompanyName = dto.CompanyName;
        config.Phone = dto.Phone;
        config.Email = dto.Email;
        config.Address = dto.Address;
        config.City = dto.City;
        config.Region = dto.Region;
        config.PostalCode = dto.PostalCode;
        config.VatPercentage = dto.VatPercentage;
        config.CurrencySymbol = dto.CurrencySymbol;
        config.LogoUrl = dto.LogoUrl;
        config.UpdatedAt = DateTime.UtcNow;
        _configurationRepository.Update(config);
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
