using Octopus.Core.Common.DynamicObject.Models;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces
{
    public interface IQueryModelFromDynamicEntity
    {
        string GetQuery(DynamicEntityWithProperties dynamicEntity);
    }
}
