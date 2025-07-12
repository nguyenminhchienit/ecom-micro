using Inventory.API.Entities;
using Inventory.API.Extensions;
using MongoDB.Driver;
using Shared.Enums.Inventory;

namespace Inventory.API.Persistence
{
    public class InventoryContextSeed
    {
        public async Task SeedDataAsync(IMongoClient mongoClient, DatabaseSettings settings)
        {
            var databaseName = settings.DatabaseName;
            var database = mongoClient.GetDatabase(databaseName);
            var inventoryCollection = database.GetCollection<InventoryEntry>("InventoryEntry");
            if (await inventoryCollection.EstimatedDocumentCountAsync() == 0)
            {
                await inventoryCollection.InsertManyAsync(GetPreconfiguredInventories());
            }
        }

        private IEnumerable<InventoryEntry> GetPreconfiguredInventories()
        {
            return new List<InventoryEntry>
        {
            new()
            {
                Quantity = 10,
                DocumentNo = Guid.NewGuid().ToString(),
                ItemNo = "Lotus",
                ExternalDocumentNo = Guid.NewGuid().ToString(),
                DocumentType = EDocumentType.Purchase
            },
            new()
            {
                ItemNo = "Cadillac",
                Quantity = 10,
                DocumentNo = Guid.NewGuid().ToString(),
                ExternalDocumentNo = Guid.NewGuid().ToString(),
                DocumentType = EDocumentType.Purchase
            },
        };
        }
    }
}
