using System.Threading.Tasks;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Migrations
{
    public class MigrationRepository : IMigrationRepository
    {
        private readonly IQueryFactoryService _queryFactory;
        private readonly IQueryHandlerService _queryHandler;

        public MigrationRepository(IQueryFactoryService queryFactory,
            IQueryHandlerService queryHandler)
        {
            _queryFactory = queryFactory;
            _queryHandler = queryHandler;
        }

        public async Task CreateSchemeAsync()
        {
            if (!await IsItemExistsAsync("INFORMATION_SCHEMA.TABLES", "table_schema", "loaderscheme"))
            {
                var query = _queryFactory.GetCreateSchemeQuery();
                await _queryHandler.ExecuteAsync(query);
            }
        }

        public async Task CreateTableAsync(DynamicEntityWithProperties dynamicEntity)
        {
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
