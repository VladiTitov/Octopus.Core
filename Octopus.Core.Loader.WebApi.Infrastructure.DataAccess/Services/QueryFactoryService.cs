using Microsoft.Extensions.Options;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Models;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services
{
    public class QueryFactoryService : IQueryFactoryService
    {
        private readonly ConnectionStringConfig _connectionString;

        public QueryFactoryService(IOptions<ConnectionStringConfig> connectionString)
        {
            _connectionString = connectionString.Value;
        }

        public string GetCreateSchemeQuery() 
            => new CreateSchemeQueryModel(_connectionString)
            .GetQuery();

        public string GetInsertQuery(DynamicEntityWithProperties dynamicEntity) 
            => new InsertQueryModel(_connectionString, dynamicEntity)
            .GetQuery();

        public string GetCreateTableQuery(DynamicEntityWithProperties dynamicEntity)
            => new CreateTableQueryModel(_connectionString, dynamicEntity)
            .GetQuery();

        public string GetCreateCommentQuery(DynamicEntityWithProperties dynamicEntity)
            => new CreateCommentQueryModel(_connectionString, dynamicEntity)
            .GetQuery();
    }
}
