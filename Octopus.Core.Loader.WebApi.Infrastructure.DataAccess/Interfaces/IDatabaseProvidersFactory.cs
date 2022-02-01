using System.Data;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces
{
    public interface IDatabaseProvidersFactory
    {
        IDbConnection CreatePostgresConnection();
        IDbConnection CreateSqlLiteConnection();
        IDbConnection CreateSqlServerConnection();
        IDbConnection CreateMySqlConnection();
        IDbConnection CreateOracleConnection();
    }
}
