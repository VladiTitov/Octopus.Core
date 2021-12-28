using Microsoft.Extensions.Options;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Common.Constants;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Common.DynamicObject.Services.Interfaces;
using Octopus.Core.Common.Extensions;
using Octopus.Core.Loader.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Octopus.Core.Loader.DataAccess.Services
{
    public class QueryFactoryService : IQueryFactoryService
    {
        private readonly ConnectionStringConfig _connectionString;
        private readonly IList<DynamicProperty> _dynamicProperties;

        public QueryFactoryService(IDynamicObjectCreateService dynamicObjectCreateService,
            IOptions<ConnectionStringConfig> connectionString)
        {
            _connectionString = connectionString.Value;
            _dynamicProperties = dynamicObjectCreateService.GetDynamicProperties(@"Configs\dynamicProperties.json");
        }

        public string GetInsertQuery(object item)
        {
            var propertyInfo = item.GetType().GetProperties();
            return $"{QueryConstants.CreateInsertQuery} {_connectionString.DbScheme}.{_connectionString.DbTable} ({propertyInfo.GetPropertiesNames()}) VALUES({propertyInfo.GetValuesNames()})";
        }

        public string GetCreateTableQuery()
        {
            var propertiesTable = _dynamicProperties.GetPropertiesNames().ToQuery(",\n");
            return $"{QueryConstants.CreateTableQuery} {QueryConstants.IfNotExistsQuery} {_connectionString.DbScheme}.{_connectionString.DbTable} ({propertiesTable});";
        }

        public string GetCreateSchemeQuery() => $"{QueryConstants.CreateSchemaQuery} {QueryConstants.IfNotExistsQuery} {_connectionString.DbScheme};";

        public string GetCreateCommentQuery()
        {
            var commentsList = _dynamicProperties.Select(property => $"{QueryConstants.CreateCommentOnColumnQuery} \"{_connectionString.DbScheme}\".\"{property.PropertyName}\" IS '{property.DynamicEntityDataBaseProperty.Comment}';").ToList();
            return commentsList.ToQuery("");
        }
    }
}
