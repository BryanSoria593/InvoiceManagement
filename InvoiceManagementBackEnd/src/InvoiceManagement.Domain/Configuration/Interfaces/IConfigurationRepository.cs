using ConfigEntity = InvoiceManagement.Domain.Configuration.Entities.Configuration;

namespace InvoiceManagement.Domain.Configuration.Interfaces;
public interface IConfigurationRepository
{
    ConfigEntity? Get();
}
