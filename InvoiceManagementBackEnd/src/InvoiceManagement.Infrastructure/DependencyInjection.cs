using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InvoiceManagement.Infrastructure.Persistence;
using InvoiceManagement.Infrastructure.Repositories;
using InvoiceManagement.Domain.Users.Interfaces;
using InvoiceManagement.Domain.Products.Interfaces;
using InvoiceManagement.Domain.Customers.Interfaces;
using InvoiceManagement.Domain.PaymentMethods.Interfaces;
using InvoiceManagement.Domain.Configuration.Interfaces;
using InvoiceManagement.Domain.Invoices.Interfaces;

namespace InvoiceManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InvoiceManagementDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();

        services.AddScoped<IConfigurationRepository, ConfigurationRepository>();

        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        return services;
    }
}
