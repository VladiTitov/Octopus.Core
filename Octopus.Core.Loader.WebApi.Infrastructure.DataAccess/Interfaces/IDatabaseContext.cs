using System.Data;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces
{
    public interface IDatabaseContext
    {
        IDbConnection CreateConnection();
    }
}
