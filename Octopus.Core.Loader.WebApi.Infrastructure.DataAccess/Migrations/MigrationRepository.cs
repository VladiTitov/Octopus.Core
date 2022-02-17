using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
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
            var query = _queryFactory.GetCreateSchemeQuery();
            await _queryHandler.ExecuteAsync(query);
        }

        public async Task CreateTableAsync(DynamicEntityWithProperties dynamicEntity)
        {
            if (!await IsItemExistsAsync(
                table: "pg_catalog.pg_namespace",
                column:"nspname", 
                value:_connectionString.Database)) 
                await CreateSchemeAsync();

            var query = _queryFactory.GetCreateTableQuery(dynamicEntity);
            await _queryHandler.ExecuteAsync(query);
        }

        public async void TableCheck(DynamicEntityWithProperties dynamicEntity)
        {
            var entityName = dynamicEntity.EntityName;
            var columns = await GetTableColumnsAsync(entityName);
            var entityProperties = dynamicEntity
                .Properties
                .Select(i=> new DynamicEntityProperty()
                {
                    PropertyName = i.PropertyName,
                    PropertyTypeName = i.SystemTypeName
                })
                .ToList();

            var list1 = entityProperties.Select(i => i.PropertyName);
            var list2 = columns.Select(i => i.PropertyName);

            var sequenceEqual = list1.SequenceEqual(list2);
        }

        public async Task<IEnumerable<DynamicEntityProperty>> GetTableColumnsAsync(string tableName)
        {
            var query = $"select column_name, data_type from information_schema.columns where table_name = '{tableName.ToLower()}';";
            return await _queryHandler.QueryListAsync<DynamicEntityProperty>(query);
        }

        private async Task<bool> IsItemExistsAsync(string table, string column, string value)
        {
            var queryExists = _queryFactory.GetExistsTableQuery(table, column, value);
            return await _queryHandler.QueryAsync<bool>(queryExists);
        }
    }
}
