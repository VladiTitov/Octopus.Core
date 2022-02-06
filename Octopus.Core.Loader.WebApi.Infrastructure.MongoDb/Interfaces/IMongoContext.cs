using MongoDB.Driver;
using Octopus.Core.Common.DynamicObject.Models;

namespace Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Interfaces
{
    public interface IMongoContext
    {
        IMongoCollection<DynamicEntityWithProperties> GetMongoCollection(string EntityName);
    }
}
