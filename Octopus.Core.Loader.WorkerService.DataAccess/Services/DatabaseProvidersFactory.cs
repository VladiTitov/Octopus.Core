using Octopus.Core.Loader.DataAccess.Interfaces;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Microsoft.Extensions.Options;

namespace Octopus.Core.Loader.DataAccess.Services
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
