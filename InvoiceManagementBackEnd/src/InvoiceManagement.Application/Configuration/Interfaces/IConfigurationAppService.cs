using InvoiceManagement.Application.Configuration.Dtos;

namespace InvoiceManagement.Application.Configuration.Interfaces;
public interface IConfigurationAppService
{
    List<ConfigurationDto> GetAllConfigurations();
    ConfigurationDto UpdateConfiguration(UpdateConfigurationDto dto);
}
