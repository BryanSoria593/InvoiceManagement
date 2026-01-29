using InvoiceManagement.Application.Configuration.Dtos;
using InvoiceManagement.Application.Configuration.Interfaces;
using InvoiceManagement.Application.Common.Interfaces;
using InvoiceManagement.Domain.Configuration.Interfaces;
using ConfigEntity = InvoiceManagement.Domain.Configuration.Entities.Configuration;

namespace InvoiceManagement.Application.Configuration.Services;

public class ConfigurationAppService : IConfigurationAppService
{
    private readonly IConfigurationRepository _configurationRepository;
    private readonly IFileStorageService _fileStorageService;

    public ConfigurationAppService(
        IConfigurationRepository configurationRepository,
        IFileStorageService fileStorageService)
    {
        _configurationRepository = configurationRepository;
        _fileStorageService = fileStorageService;
    }

    public ConfigurationDto? GetSingleConfiguration()
    {
        var config = _configurationRepository.GetAll().FirstOrDefault();
        if (config == null) return null;

        var dto = MapToDto(config);
        SetLogoBase64IfExists(dto);
        return dto;
    }

    public List<ConfigurationDto> GetAllConfigurations()
    {
        return _configurationRepository
            .GetAll()
            .Select(MapToDto)
            .ToList();
    }

    public ConfigurationDto UpdateConfiguration(UpdateConfigurationDto dto)
    {
        var config = _configurationRepository.GetAll().FirstOrDefault()
            ?? throw new Exception("No configuration found to update");

        config.CompanyName = dto.CompanyName;
        config.Phone = dto.Phone;
        config.Email = dto.Email;
        config.Address = dto.Address;
        config.City = dto.City;
        config.Region = dto.Region;
        config.PostalCode = dto.PostalCode;
        config.VatPercentage = dto.VatPercentage;
        config.CurrencySymbol = dto.CurrencySymbol;

        if (!string.IsNullOrWhiteSpace(dto.Base64LogoImage))
        {
            var logoPath = _fileStorageService.SaveBase64LogoImage(dto.Base64LogoImage);
            config.LogoUrl = logoPath;
        }
        else if (!string.IsNullOrWhiteSpace(dto.LogoUrl))
        {
            config.LogoUrl = dto.LogoUrl;
        }

        config.UpdatedAt = DateTime.UtcNow;

        _configurationRepository.Update(config);

        var result = MapToDto(config);
        SetLogoBase64IfExists(result);
        return result;
    }

    private static ConfigurationDto MapToDto(ConfigEntity config)
    {
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

    private static void SetLogoBase64IfExists(ConfigurationDto dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.LogoUrl))
            return;

        var filePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            dto.LogoUrl.TrimStart('/')
                .Replace("/", Path.DirectorySeparatorChar.ToString())
        );

        if (!File.Exists(filePath))
            return;

        var bytes = File.ReadAllBytes(filePath);
        var ext = Path.GetExtension(filePath).ToLower();

        var mime = ext switch
        {
            ".png" => "image/png",
            ".jpg" or ".jpeg" => "image/jpeg",
            _ => "application/octet-stream"
        };

        dto.Base64LogoImage = $"data:{mime};base64,{Convert.ToBase64String(bytes)}";
    }
}
