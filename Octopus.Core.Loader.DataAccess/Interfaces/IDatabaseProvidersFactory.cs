using System.Data;

namespace Octopus.Core.Loader.DataAccess.Interfaces
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
