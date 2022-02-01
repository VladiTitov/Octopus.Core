using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using MySqlConnector;
using Npgsql;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Oracle.ManagedDataAccess.Client;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services
{
    public class DatabaseProvidersFactory : IDatabaseProvidersFactory
    {
        private readonly string _connectionString;
        public DatabaseProvidersFactory(IOptions<ConnectionStringConfig> connectionString)
        {
            _connectionString = connectionString.Value.ToString();
        }

        public IDbConnection CreatePostgresConnection() => new NpgsqlConnection(_connectionString);

        public IDbConnection CreateSqlLiteConnection() => new SqliteConnection(_connectionString);

        public IDbConnection CreateSqlServerConnection() => new SqlConnection(_connectionString);

        public IDbConnection CreateMySqlConnection() => new MySqlConnection(_connectionString);

        public IDbConnection CreateOracleConnection() => new OracleConnection(_connectionString);
    }
}
