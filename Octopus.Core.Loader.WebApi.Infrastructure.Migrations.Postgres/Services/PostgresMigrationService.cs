using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Models;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Services
{
    public class PostgresMigrationService : IPostgresMigrationService
    {
        private readonly ConnectionStringConfig _connectionString;
        private readonly IQueryFactoryService _queryFactory;
        private readonly IQueryHandlerService _queryHandler;

        public PostgresMigrationService(IOptions<ConnectionStringConfig> connectionString,
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
                column: "nspname",
                value: _connectionString.Database))
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
                .Select(i => new TableColumn()
                {
                    PropertyName = i.PropertyName,
                    PropertyTypeName = i.SystemTypeName
                })
                .ToList();

            var list1 = entityProperties.Select(i => i.PropertyName);
            var list2 = columns.Select(i => i.PropertyName);

            var sequenceEqual = list1.SequenceEqual(list2);
        }

        public async Task<IEnumerable<TableColumn>> GetTableColumnsAsync(string tableName)
        {
            var query = $"select column_name, data_type from information_schema.columns where table_name = '{tableName.ToLower()}';";
            return await _queryHandler.QueryListAsync<TableColumn>(query);
        }

        private async Task<bool> IsItemExistsAsync(string table, string column, string value)
        {
            var queryExists = _queryFactory.GetExistsTableQuery(table, column, value);
            return await _queryHandler.QueryAsync<bool>(queryExists);
        }
    }
}

