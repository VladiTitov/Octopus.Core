using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Constants;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Models;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Services;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Constants;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Models
{
    public class TableHandler : ITableHandler
    {
        private readonly IQueryFactoryService _queryFactory;
        private readonly IQueryHandlerService _queryHandler;

        public TableHandler(IQueryFactoryService queryFactory,
            IQueryHandlerService queryHandler)
        {
            _queryFactory = queryFactory;
            _queryHandler = queryHandler;
        }

        public async Task CreateTableAsync(DynamicEntityWithProperties dynamicEntity)
        {
            var query = _queryFactory.GetCreateTableQuery(dynamicEntity);
            await _queryHandler.ExecuteAsync(query);
        }

        public async Task TableCheck(DynamicEntityWithProperties dynamicEntity)
        {
            var columnsList = await GetColumnsFromTableAsync(dynamicEntity.EntityName);
            var entityProperties = GetColumnsFromDynamicEntity(dynamicEntity);
        }

        private IEnumerable<TableColumn> GetColumnsFromDynamicEntity(DynamicEntityWithProperties dynamicEntity)
        {
            return dynamicEntity.Properties.Select(i
                => new TableColumn()
                {
                    PropertyName = i.PropertyName,
                    PropertyTypeName = i.SystemTypeName
                });
        }

        private async Task<IEnumerable<TableColumn>> GetColumnsFromTableAsync(string entityName) 
            => await GetTableColumnsAsync(entityName);

        public async Task<IEnumerable<TableColumn>> GetTableColumnsAsync(string tableName)
        {
            using (var queryBuilder = new QueryBuilderService())
            {
                var query = queryBuilder
                    .AddPart(QueryConstants.Select)
                    .AddPart(PostgresConstants.ColumnName)
                    .AddSeparator(",")
                    .AddPart(PostgresConstants.DataType)
                    .AddPart(QueryConstants.From)
                    .AddPart(PostgresConstants.DatabaseColumnsList)
                    .AddPart(QueryConstants.Where)
                    .AddPart(PostgresConstants.TableName)
                    .AddSeparator(" = ")
                    .AddPart($"'{tableName.ToLower()}';")
                    .GetQuery();

                return await _queryHandler.QueryListAsync<TableColumn>(query);
            }
        }
    }
}
