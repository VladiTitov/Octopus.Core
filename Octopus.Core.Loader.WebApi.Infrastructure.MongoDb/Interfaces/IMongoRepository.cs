using MongoDB.Driver;
using Octopus.Core.Common.DynamicObject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Interfaces
{
    public interface IMongoRepository
    {
        Task Add(DynamicEntityWithProperties item);
    }
}
