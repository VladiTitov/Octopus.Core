using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Interfaces;
using Octopus.Core.Common.DynamicObject.Models;

namespace Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Context
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(IOptions<MongoDatabaseConfiguration> mongoDatabaseConfiguration)
        {
            var client = new MongoClient(mongoDatabaseConfiguration.Value.ConnectionString);
            _database = client.GetDatabase(mongoDatabaseConfiguration.Value.DatabaseName);
        }

        public IMongoCollection<DynamicEntityWithProperties> GetMongoCollection(string entityName) => 
            _database.GetCollection<DynamicEntityWithProperties>(entityName);
    }
}
