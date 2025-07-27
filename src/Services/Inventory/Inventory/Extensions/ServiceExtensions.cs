using Infrastructure.Extensions;
using Inventory.API.Services;
using Inventory.API.Services.Interfaces;
using MongoDB.Driver;
using Shared.Configurations;

namespace Inventory.API.Extensions
{
    public static class ServiceExtensions
    {

        public static IServiceCollection AddConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseSetting = configuration.GetSection(nameof(MongoDbSetting)).Get<MongoDbSetting>();
            services.AddSingleton(databaseSetting);

            return services;
        }
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));

            services.AddScoped<IInventoryService, InventoryService>();

            services.ConfigureMongoDbClient();
            return services;
        }

        private static string getMongoConnectionString(this IServiceCollection services)
        {
            var settings = services.GetOptions<MongoDbSetting>(nameof(MongoDbSetting));
            if (settings == null || string.IsNullOrEmpty(settings.ConnectionString))
                throw new ArgumentNullException("DatabaseSettings is not configured");

            var databaseName = settings.DatabaseName;
            var mongodbConnectionString = settings.ConnectionString + "/" + databaseName +
                                          "?authSource=admin";
            return mongodbConnectionString;
        }

        public static void ConfigureMongoDbClient(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(
                new MongoClient(getMongoConnectionString(services)))
                .AddScoped(x => x.GetService<IMongoClient>()?.StartSession());
        }


    }
}
