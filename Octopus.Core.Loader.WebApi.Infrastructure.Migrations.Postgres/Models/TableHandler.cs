using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Constants;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Services;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Extensions;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Interfaces;
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
            var entityProperties = GetColumnsFromDynamicEntity(dynamicEntity).ToList();

            if (!columnsList.IsEquals(entityProperties))
            {
                //Необходимо обновить таблицу
            }
        }

        private IEnumerable<ITableColumn> GetColumnsFromDynamicEntity(DynamicEntityWithProperties dynamicEntity) 
            => dynamicEntity
                .Properties
                .Select(i 
                    => new PostgresTableColumn()
                    {
                        PropertyName = i.PropertyName,
                        PropertyTypeName = i.SystemTypeName,
                        PropertyIsNullable = i.DynamicEntityDataBaseProperty.IsNotNull

                    });
        
        private async Task<IEnumerable<ITableColumn>> GetColumnsFromTableAsync(string tableName)
        {
            using (var queryBuilder = new QueryBuilderService())
            {
                var query = queryBuilder
                    .AddPart(QueryConstants.Select)
                    .AddPart(PostgresConstants.ColumnName)
                    .AddSeparator(",")
                    .AddPart(PostgresConstants.DataType)
                    .AddSeparator(",")
                    .AddPart(PostgresConstants.IsNullable)
                    .AddPart(QueryConstants.From)
                    .AddPart(PostgresConstants.DatabaseColumnsList)
                    .AddPart(QueryConstants.Where)
                    .AddPart(PostgresConstants.TableName)
                    .AddSeparator(" = ")
                    .AddPart($"'{tableName.ToLower()}' ")
                    .AddPart(" order by ordinal_position;")
                    .GetQuery();

                return await _queryHandler.QueryListAsync<PostgresTableColumn>(query);
            }

        }
    }
}
