using System.Threading.Tasks;
using MongoDB.Driver;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Repositories
{
    public class MongoRepository : IMongoRepository
    {
        private readonly IMongoContext _mongoContext;

        public MongoRepository(IMongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task AddEntity(DynamicEntityWithProperties item)
        {
            var collection = _mongoContext.GetMongoCollection(item.EntityName);
            await collection.InsertOneAsync(item);
        }

        public async Task<DynamicEntityWithProperties> GetEntity(string entityName)
        {
            var collection = _mongoContext.GetMongoCollection(entityName);
            return await collection.Find(x => x.EntityName.Equals(entityName)).FirstOrDefaultAsync();
        }
    }
}
