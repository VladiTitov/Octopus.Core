using System.Threading.Tasks;
using Octopus.Core.Common.DynamicObject.Models;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Interfaces
{
    public interface ITableHandler
    {
        Task TableCheck(DynamicEntityWithProperties dynamicEntity);
        Task CreateTableAsync(DynamicEntityWithProperties dynamicEntity);
    }
}
