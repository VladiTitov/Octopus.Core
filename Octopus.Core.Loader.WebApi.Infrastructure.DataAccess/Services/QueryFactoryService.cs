using System.Linq;
using Microsoft.Extensions.Options;
using Octopus.Core.Common.Constants;
using Octopus.Core.Common.Extensions;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services
{
    public class QueryFactoryService : IQueryFactoryService
    {
        private readonly ConnectionStringConfig _connectionString;

        public QueryFactoryService(IOptions<ConnectionStringConfig> connectionString)
        {
            _connectionString = connectionString.Value;
        }

        public string GetInsertQuery(DynamicEntityWithProperties dynamicEntity)
        {
            var properties = dynamicEntity
                .Properties
                .Select(i => i.PropertyName)
                .ToList();

            return $"{QueryConstants.CreateInsertQuery} " +
                   $"{_connectionString.DbScheme}.{dynamicEntity.EntityName} " +
                   $"({properties.GetPropertiesNames()}) " +
                   $"VALUES({properties.GetValuesNames()})";
        }

        public string GetCreateTableQuery(DynamicEntityWithProperties dynamicEntity)
        {
            var propertiesTable = dynamicEntity.Properties
                .GetPropertiesNames()
                .ToQuery(",\n");

            return $"{QueryConstants.CreateTableQuery} " +
                   $"{QueryConstants.IfNotExistsQuery} " +
                   $"{_connectionString.DbScheme}.{dynamicEntity.EntityName} " +
                   $"({propertiesTable});";
        }

        public string GetCreateSchemeQuery() => $"{QueryConstants.CreateSchemaQuery} " +
                                                $"{QueryConstants.IfNotExistsQuery} " +
                                                $"{_connectionString.DbScheme};";

        public string GetCreateCommentQuery(DynamicEntityWithProperties dynamicEntity)
        {
            var commentsList = dynamicEntity.Properties
                .Select(property => $"{QueryConstants.CreateCommentOnColumnQuery} " +
                                    $"\"{_connectionString.DbScheme}\".\"{property.PropertyName}\" " +
                                    $"IS '{property.DynamicEntityDataBaseProperty.Comment}';").ToList();

            return commentsList.ToQuery("");
        }
    }
}
