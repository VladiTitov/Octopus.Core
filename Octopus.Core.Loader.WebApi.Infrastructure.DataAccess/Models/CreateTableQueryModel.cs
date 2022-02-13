using Microsoft.Extensions.Options;
using Octopus.Core.Common.Constants;
using Octopus.Core.Common.Extensions;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Models
{
    public class CreateTableQueryModel : IQueryModelFromDynamicEntity
    {
        private ConnectionStringConfig _connectionString;

        public CreateTableQueryModel(IOptions<ConnectionStringConfig> connectionString)
        {
            _connectionString = connectionString.Value;
        }

        public string GetQuery(DynamicEntityWithProperties dynamicEntity)
        {
            var propertiesTable = dynamicEntity.Properties
                .GetPropertiesNames()
                .ToQuery(",\n");

            using (var queryBuilder = new QueryBuilderService())
            {
                return queryBuilder
                    .AddPart(QueryConstants.CreateTableQuery)
                    .AddSeparator(" ")
                    .AddPart(QueryConstants.IfNotExistsQuery)
                    .AddSeparator(" ")
                    .AddPart(_connectionString.DbScheme)
                    .AddSeparator(".")
                    .AddPart(dynamicEntity.EntityName)
                    .AddSeparator(" ")
                    .AddPart($"({propertiesTable});")
                    .GetQuery();
            }     
        }
    }
}
