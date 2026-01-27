using InvoiceManagement.Application.Configuration.Dtos;

namespace InvoiceManagement.Application.Configuration.Interfaces;
public interface IConfigurationAppService
{
    ConfigurationDto? GetConfiguration();
}
