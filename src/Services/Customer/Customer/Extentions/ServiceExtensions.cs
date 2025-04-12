using Contract.Common.Interfaces;
using Customer.API.Persistence;
using Customer.API.Repositories.Interfaces;
using Customer.API.Repositories;
using Customer.API.Services.Interfaces;
using Customer.API.Services;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.ConfigureProductDbContext(configuration);
        services.AddInfrastructureServices();
   

        return services;
    }

    private static IServiceCollection ConfigureProductDbContext(this IServiceCollection services, IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString("DefaultConnectionString");
        services.AddDbContext<CustomerContext>(
            options => options.UseNpgsql(connectionString));
        services.AddScoped<ICustomerRepository, CustomerRepository>()
            .AddScoped(typeof(IRepositoryBaseAsync<,,>), typeof(RepositoryBaseAsyncAsync<,,>))
            .AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
            .AddScoped<ICustomerService, CustomerService>();
        return services;
    }

    private static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        return services.AddScoped(typeof(IRepositoryBaseAsync<,,>), typeof(RepositoryBaseAsyncAsync<,,>))
                .AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
            ;
    }
}