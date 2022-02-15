using System.Threading.Tasks;
using Octopus.Core.Common.DynamicObject.Models;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces
{
    public interface IMigrationCreateService
    {
        Task CreateMigrationAsync(DynamicEntityWithProperties entity);
        Task CreateTableAsync();
        Task CreateSchemeAsync();
    }
}
