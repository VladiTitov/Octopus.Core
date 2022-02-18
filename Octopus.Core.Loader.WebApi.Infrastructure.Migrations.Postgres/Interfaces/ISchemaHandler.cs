using System.Threading.Tasks;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Interfaces
{
    public interface ISchemaHandler
    {
        Task CreateSchemeAsync();
        Task<bool> IsExistSchema();
    }
}
