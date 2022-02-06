using System;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Interfaces;
using Octopus.Core.Common.DynamicObject.Models;

namespace Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Context
{
    public class MongoContext : IMongoContext, IDisposable
    {
        public IMongoDatabase Database { get; set; }
        public IMongoCollection<DynamicEntityWithProperties> Collection { get; set; }

        public MongoContext(IOptions<MongoDatabaseConfiguration> mongoDatabaseConfiguration)
        {
            var client = new MongoClient(mongoDatabaseConfiguration.Value.ConnectionString);

            Database = client.GetDatabase(mongoDatabaseConfiguration.Value.DataBaseName);
            Collection = Database.GetCollection<DynamicEntityWithProperties>(mongoDatabaseConfiguration.Value.CollectionName);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
