using Inventory.API.Entities.Abstraction;
using Inventory.API.Extensions;
using Inventory.API.Extensions.Attributes;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Inventory.API.Repositories.Abstraction
{
    public class MongoDbRepository<T> : IMongoDbRepositoryBase<T> where T : MongoEntity
    {
        public IMongoDatabase Database { get; }

        public MongoDbRepository(IMongoClient client, DatabaseSettings settings)
        {
            Database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<T> FindAll(ReadPreference? readPreference = null)
        {
            return Database
                .WithReadPreference(readPreference ?? ReadPreference.Primary)
                .GetCollection<T>(GetCollectionName<T>());
        }

        protected virtual IMongoCollection<T> Collection =>
            Database.GetCollection<T>(GetCollectionName<T>());

        public Task CreateAsync(T entity) => Collection.InsertOneAsync(entity);

        public Task UpdateAsync(T entity)
        {
            Expression<Func<T, string>> func = f => f.Id.ToString();
            var value = (string)entity.GetType()
                .GetProperty(func.Body.ToString()
                    .Split(".")[1])?.GetValue(entity, null).ToString();
            var filter = Builders<T>.Filter.Eq(func, value);

            return Collection.ReplaceOneAsync(filter, entity);
        }

        public Task DeleteAsync(string id) => Collection.DeleteOneAsync(x => x.Id.Equals(id));

        private static string GetCollectionName<T>()
        {
            return (typeof(T).GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault() as
                BsonCollectionAttribute)?.CollectionName;
        }
    }
}
