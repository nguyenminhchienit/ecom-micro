using Inventory.API.Entities;
using MongoDB.Driver;

namespace Inventory.API.Persistence.Interfaces
{
    public interface IInventoryContext
    {
        IMongoCollection<InventoryEntry> InventoryEntries { get; }
    }
}
