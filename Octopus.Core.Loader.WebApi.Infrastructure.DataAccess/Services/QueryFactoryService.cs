using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Common.Constants;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Common.Extensions;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using System.Threading.Tasks;
using Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services
{
    public class QueryFactoryService : IQueryFactoryService
    {
        private readonly ConnectionStringConfig _connectionString;
        private readonly IMongoRepository _mongoRepository;

        public QueryFactoryService(IOptions<ConnectionStringConfig> connectionString,
            IMongoRepository mongoRepository)
        {
            _connectionString = connectionString.Value;
            _mongoRepository = mongoRepository;
        }

        private async Task<IList<DynamicProperty>> GetDynamicProperties(string entityName)
        {
            var dynamicEntity = await _mongoRepository.GetEntity(entityName);
            return dynamicEntity.Properties;
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

        public async Task<string> GetCreateTableQuery(string entityName)
        {
            var dynamicProperties = await GetDynamicProperties(entityName);
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

        public async Task<string> GetCreateCommentQuery(string entityName)
        {
            var dynamicProperties = await GetDynamicProperties(entityName);
            var commentsList = dynamicProperties
                .Select(property => $"{QueryConstants.CreateCommentOnColumnQuery} " +
                                    $"\"{_connectionString.DbScheme}\".\"{property.PropertyName}\" " +
                                    $"IS '{property.DynamicEntityDataBaseProperty.Comment}';").ToList();

            return commentsList.ToQuery("");
        }
    }
}
