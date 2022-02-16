using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Migrations
{
    public class MigrationRepository : IMigrationRepository
    {
        private readonly ConnectionStringConfig _connectionString;
        private readonly IQueryFactoryService _queryFactory;
        private readonly IQueryHandlerService _queryHandler;

        public MigrationRepository(IOptions<ConnectionStringConfig> connectionString,
            IQueryFactoryService queryFactory,
            IQueryHandlerService queryHandler)
        {
            _connectionString = connectionString.Value;
            _queryFactory = queryFactory;
            _queryHandler = queryHandler;
        }

        public async Task CreateSchemeAsync()
        {
            if (!await IsItemExistsAsync("pg_catalog.pg_namespace", "nspname", _connectionString.Database))
            {
                var query = _queryFactory.GetCreateSchemeQuery();
                await _queryHandler.ExecuteAsync(query);
            }
        }

        public async Task CreateTableAsync(DynamicEntityWithProperties dynamicEntity)
        {
            await CreateSchemeAsync();

            var query = _queryFactory.GetCreateTableQuery(dynamicEntity);
            await _queryHandler.ExecuteAsync(query);
        }

        private async Task<bool> IsItemExistsAsync(string table, string column, string value)
        {
            var queryExists = _queryFactory.GetExistsTableQuery(table, column, value);
            return await _queryHandler.QueryAsync<bool>(queryExists);
        }
    }
}
