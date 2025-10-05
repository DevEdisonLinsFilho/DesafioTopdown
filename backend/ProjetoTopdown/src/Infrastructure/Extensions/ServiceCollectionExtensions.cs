using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using ProjetoTopdown.Application.Interfaces;
using ProjetoTopdown.Application.Interfaces.Repositories;
using ProjetoTopdown.Infrastructure.Persistence;
using ProjetoTopdown.Infrastructure.Persistence.Repositories.CustomerPersistence;
using ProjetoTopdown.Infrastructure.Persistence.Repositories.OrderPersistence;
using ProjetoTopdown.Infrastructure.Persistence.Repositories.ProductPersistence;
using System.Data;

namespace ProjetoTopdown.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddDbRepositories(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IDbConnection>(provider => new NpgsqlConnection(connectionString));

        services.AddScoped<IDapperWrapper, DapperWrapper>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();


    }
}