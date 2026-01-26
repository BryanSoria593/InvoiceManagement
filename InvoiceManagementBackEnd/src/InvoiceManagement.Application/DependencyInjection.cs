using InvoiceManagement.Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserAppService, UserAppService>();
        return services;
    }
}
