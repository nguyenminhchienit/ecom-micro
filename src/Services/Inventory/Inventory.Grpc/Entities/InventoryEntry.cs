using Contract.Domain;
using Infrastructure.Extensions.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Inventory.Grpc.Entities
{
    [BsonCollection("InventoryEntry")]
    public class InventoryEntry : MongoEntity
    {
        [BsonElement("itemNo")]
        public string ItemNo { get; set; }

        [BsonElement("quantity")]
        public int Quantity { get; set; }
    }
}
