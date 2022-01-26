using System.Threading.Tasks;

namespace Octopus.Core.Loader.DataAccess.Interfaces
{
    public interface IMigrationCreateService
    {
        Task CreateMigrationAsync();
        Task CreateTableAsync();
        Task CreateSchemeAsync();
    }
}
