using System.Threading.Tasks;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces
{
    public interface IMigrationCreateService
    {
        Task CreateMigrationAsync(string entityName);
        Task CreateTableAsync(string entityName);
        Task CreateSchemeAsync(string entityName);
    }
}
