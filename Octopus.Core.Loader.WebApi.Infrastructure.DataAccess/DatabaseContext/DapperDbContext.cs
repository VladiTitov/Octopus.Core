using System.Data;
using Microsoft.Extensions.Options;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Constants;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.DatabaseContext
{
    public class DapperDbContext : IDatabaseContext
    {
        private readonly ConnectionStringConfig _connectionString;
        private readonly IDatabaseProvidersFactory _providersFactory;

        public DapperDbContext(
            IOptions<ConnectionStringConfig> connectionString,
            IDatabaseProvidersFactory databaseProviders)
        {
            _connectionString = connectionString.Value;
            _providersFactory = databaseProviders;
        }

        public IDbConnection CreateConnection()
        {
            return _connectionString.DbType switch
            {
                ProvidersNamesConstants.PostgreSql => _providersFactory.CreatePostgresConnection(),
                ProvidersNamesConstants.SqlLite => _providersFactory.CreateSqlLiteConnection(),
                ProvidersNamesConstants.SqlServer => _providersFactory.CreateSqlServerConnection(),
                ProvidersNamesConstants.MySql => _providersFactory.CreateMySqlConnection(),
                ProvidersNamesConstants.Oracle => _providersFactory.CreateOracleConnection(),
                _ => null
            };
        }
    }
}
