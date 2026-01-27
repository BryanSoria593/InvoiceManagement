using InvoiceManagement.Application.Configuration.Interfaces;
using InvoiceManagement.Application.Configuration.Services;
using InvoiceManagement.Application.Customers.Interfaces;
using InvoiceManagement.Application.Customers.Services;
using InvoiceManagement.Application.PaymentMethods.Interfaces;
using InvoiceManagement.Application.PaymentMethods.Services;
using InvoiceManagement.Application.Products.Interfaces;
using InvoiceManagement.Application.Products.Services;
using InvoiceManagement.Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserAppService, UserAppService>();
        services.AddScoped<IProductAppService, ProductAppService>();
        services.AddScoped<ICustomerAppService, CustomerAppService>();
        services.AddScoped<IPaymentMethodAppService, PaymentMethodAppService>();
        services.AddScoped<IConfigurationAppService, ConfigurationAppService>();
        return services;
    }
}
