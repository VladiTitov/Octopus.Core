using Microsoft.Extensions.Options;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Loader.DataAccess.Interfaces;
using System.Data;

namespace Octopus.Core.Loader.DataAccess.DatabaseContext
{
    public class DapperDbContext : IDatabaseContext
    {
        private readonly ConnectionStringConfig _connectionString;
        private readonly IDatabaseProvidersFactory _providersFactory;

        public DapperDbContext(IOptions<ConnectionStringConfig> connectionString, 
            IDatabaseProvidersFactory databaseProviders)
        {
            _connectionString = connectionString.Value;
            _providersFactory = databaseProviders;
        }

        public IDbConnection CreateConnection()
        {
            return _connectionString.DbType switch
            {
                "PostgreSql" => _providersFactory.CreatePostgresConnection(),
                "SqlLite" => _providersFactory.CreateSqlLiteConnection(),
                "SqlServer" => _providersFactory.CreateSqlServerConnection(),
                "MySql" => _providersFactory.CreateMySqlConnection(),
                "Oracle" => _providersFactory.CreateOracleConnection(),
                _ => null
            };
        }
    }
}
