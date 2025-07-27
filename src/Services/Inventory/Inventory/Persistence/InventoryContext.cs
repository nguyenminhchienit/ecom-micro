using Inventory.API.Entities;
using Inventory.API.Extensions;
using Inventory.API.Persistence.Interfaces;
using MongoDB.Driver;
using Shared.Configurations;

namespace Inventory.API.Persistence
{
    public class InventoryContext : IInventoryContext
    {
        private readonly MongoDbSetting _settings;
        private readonly IMongoClient _mongoClient;

        public InventoryContext(IMongoClient mongoClient, MongoDbSetting settings)
        {
            _settings = settings;
            _mongoClient = mongoClient;

            if (!_mongoClient.GetDatabase(_settings.DatabaseName)
                    .ListCollectionNames().ToList()
                    .Contains("InventoryEntry"))
            {
                _mongoClient.GetDatabase(_settings.DatabaseName).CreateCollection("InventoryEntry");
            }

            InventoryEntries = mongoClient.GetDatabase(_settings.DatabaseName).GetCollection<InventoryEntry>("InventoryEntry");
        }

        public IMongoCollection<InventoryEntry> InventoryEntries { get; }
    }
}
