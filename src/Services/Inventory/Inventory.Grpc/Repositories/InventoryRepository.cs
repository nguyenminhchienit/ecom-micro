using Infrastructure.Common;
using Inventory.Grpc.Entities;
using Inventory.Grpc.Repositories.Interfaces;
using MongoDB.Driver;
using Shared.Configurations;

namespace Inventory.Grpc.Repositories
{
    public class InventoryRepository : MongoDbRepository<InventoryEntry>, IInventoryRepository
    {
        public InventoryRepository(IMongoClient client, MongoDbSetting settings) : base(client, settings)
        {
        }

        public async Task<int> GetStockQuantity(string itemNo)
        {
            var result = Collection.AsQueryable().Where(x => x.ItemNo == itemNo)
                .Sum(x => x.Quantity);
            return result;
        }
    }
}
