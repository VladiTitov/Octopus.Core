using System.Linq;
using System.Collections.Generic;
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

        public string GetInsertQuery(object item, string entityName)
        {
            var propertyInfo = item
                .GetType()
                .GetProperties();
            return $"{QueryConstants.CreateInsertQuery} " +
                   $"{_connectionString.DbScheme}.{entityName} " +
                   $"({propertyInfo.GetPropertiesNames()}) " +
                   $"VALUES({propertyInfo.GetValuesNames()})";
        }

        public string GetCreateTableQuery(string entityName, IList<DynamicProperty> dynamicProperties)
        {
            var propertiesTable = dynamicProperties
                .GetPropertiesNames()
                .ToQuery(",\n");
            return $"{QueryConstants.CreateTableQuery} " +
                   $"{QueryConstants.IfNotExistsQuery} " +
                   $"{_connectionString.DbScheme}.{entityName} " +
                   $"({propertiesTable});";
        }

        public string GetCreateSchemeQuery() => $"{QueryConstants.CreateSchemaQuery} " +
                                                $"{QueryConstants.IfNotExistsQuery} " +
                                                $"{_connectionString.DbScheme};";

        public string GetCreateCommentQuery(string entityName, IList<DynamicProperty> dynamicProperties)
        {
            var commentsList = dynamicProperties
                .Select(property => $"{QueryConstants.CreateCommentOnColumnQuery} " +
                                    $"\"{_connectionString.DbScheme}\".\"{property.PropertyName}\" " +
                                    $"IS '{property.DynamicEntityDataBaseProperty.Comment}';").ToList();

            return commentsList.ToQuery("");
        }
    }
}
