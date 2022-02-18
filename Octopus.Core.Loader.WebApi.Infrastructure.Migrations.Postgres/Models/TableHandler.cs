using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Constants;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Models;
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
            var query = $"{QueryConstants.Select}" +
                        $"{PostgresConstants.ColumnName}," +
                        $"{PostgresConstants.DataType}" +
                        $"{QueryConstants.From}" +
                        $"{PostgresConstants.DatabaseColumnsList} " +
                        $"{QueryConstants.Where}" +
                        $"{PostgresConstants.TableName} = " +
                        $"'{tableName.ToLower()}';";

            return await _queryHandler.QueryListAsync<TableColumn>(query);
        }
    }
}
