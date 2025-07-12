using Contract.Domain;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Inventory.API.Entities.Abstraction
{
    public abstract class MongoEntity : EntityBase<ObjectId> 
    { 
        [BsonElement("createdDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)] 
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; 
        
        [BsonElement("lastModifiedDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)] 
        public DateTime? LastModifiedDate { get; set; } 
    }
}
