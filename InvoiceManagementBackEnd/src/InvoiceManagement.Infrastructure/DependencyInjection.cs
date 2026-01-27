using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InvoiceManagement.Infrastructure.Persistence;
using InvoiceManagement.Infrastructure.Repositories;
using InvoiceManagement.Domain.Users.Interfaces;
using InvoiceManagement.Domain.Products.Interfaces;
using InvoiceManagement.Domain.Customers.Interfaces;

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

        return services;
    }
}
