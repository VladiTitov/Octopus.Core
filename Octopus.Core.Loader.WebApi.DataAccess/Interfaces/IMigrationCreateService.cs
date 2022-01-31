using System.Threading.Tasks;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces
{
    public interface IMigrationCreateService
    {
        Task CreateMigrationAsync();
        Task CreateTableAsync();
        Task CreateSchemeAsync();
    }
}
