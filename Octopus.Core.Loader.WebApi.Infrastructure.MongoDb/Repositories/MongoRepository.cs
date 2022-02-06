using System.Collections.Generic;
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

        public async Task Add(DynamicEntityWithProperties item)
        {
            await _mongoContext.Collection.InsertOneAsync(item);
        }
    }
}
