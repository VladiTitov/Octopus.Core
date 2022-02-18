using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Constants;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Models
{
    public class SchemaHandler : ISchemaHandler
    {
        private readonly ConnectionStringConfig _connectionString;
        private readonly IQueryFactoryService _queryFactory;
        private readonly IQueryHandlerService _queryHandler;

        public SchemaHandler(IOptions<ConnectionStringConfig> connectionString,
            IQueryFactoryService queryFactory,
            IQueryHandlerService queryHandler)
        {
            _connectionString = connectionString.Value;
            _queryFactory = queryFactory;
            _queryHandler = queryHandler;
        }

        public async Task CreateSchemeAsync()
        {
            var query = _queryFactory.GetCreateSchemeQuery();
            await _queryHandler.ExecuteAsync(query);
        }

        public async Task<bool> IsExistSchema() =>
            await IsItemExistsAsync(
                table: PostgresConstants.DatabaseSchemaList,
                column: PostgresConstants.AllSchemesColumn,
                value: _connectionString.Database);

        private async Task<bool> IsItemExistsAsync(string table, string column, string value)
        {
            var queryExists = _queryFactory.GetExistsTableQuery(table, column, value);
            return await _queryHandler.QueryAsync<bool>(queryExists);
        }
    }
}
