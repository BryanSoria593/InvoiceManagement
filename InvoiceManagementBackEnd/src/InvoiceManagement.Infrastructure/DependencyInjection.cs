using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InvoiceManagement.Infrastructure.Persistence;
using InvoiceManagement.Infrastructure.Repositories;
using InvoiceManagement.Domain.Users.Interfaces;

namespace InvoiceManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InvoiceManagementDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // User Repositories
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
