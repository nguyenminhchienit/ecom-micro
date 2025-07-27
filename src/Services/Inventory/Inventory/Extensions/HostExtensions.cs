using Inventory.API.Persistence;
using MongoDB.Driver;
using Shared.Configurations;

namespace Inventory.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var settings = services.GetService<MongoDbSetting>();
            var mongoClient = services.GetRequiredService<IMongoClient>();
            new InventoryContextSeed()
                .SeedDataAsync(mongoClient, settings)
                .Wait();
            return host;
        }
    }
}
