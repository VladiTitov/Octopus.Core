using System.Threading.Tasks;
using Octopus.Core.Common.DynamicObject.Models;

namespace Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Interfaces
{
    public interface IMongoRepository
    {
        Task AddEntity(DynamicEntityWithProperties item);
        Task<DynamicEntityWithProperties> GetEntity(string entityName);
    }
}
