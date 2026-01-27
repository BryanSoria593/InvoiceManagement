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
        return services;
    }
}
