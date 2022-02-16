using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Common.Exceptions;
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
            try
            {
                var collection = _mongoContext.GetMongoCollection(item.EntityName);
                await collection.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                throw new MongoDbException(ex.Message);
            }
        }

        public async Task<DynamicEntityWithProperties> GetEntity(string entityName)
        {
            try
            {
                var collection = _mongoContext.GetMongoCollection(entityName);
                return await collection
                    .Find(x => x.EntityName.Equals(entityName))
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new MongoDbException(ex.Message);
            }
        }
    }
}
