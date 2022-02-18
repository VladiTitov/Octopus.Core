using System.Threading.Tasks;
using Octopus.Core.Common.DynamicObject.Models;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Interfaces
{
    public interface IPostgresMigrationService
    {
        Task CreateSchemeAsync();
        Task CreateTableAsync(DynamicEntityWithProperties dynamicEntity);
        Task TableCheckAsync(DynamicEntityWithProperties dynamicEntity);
    }
}
