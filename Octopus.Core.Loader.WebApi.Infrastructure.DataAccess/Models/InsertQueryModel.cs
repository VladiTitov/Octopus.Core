using System.Linq;
using Microsoft.Extensions.Options;
using Octopus.Core.Common.Constants;
using Octopus.Core.Common.Extensions;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Models
{
    public class InsertQueryModel : IQueryModelFromDynamicEntity
    {
        private ConnectionStringConfig _connectionString;

        public InsertQueryModel(IOptions<ConnectionStringConfig> connectionString)
        {
            _connectionString = connectionString.Value;
        }

        public string GetQuery(DynamicEntityWithProperties dynamicEntity)
        {
            var properties = dynamicEntity
                .Properties
                .Select(i => i.PropertyName)
                .ToList();

            using (var quieryBuilder = new QueryBuilderService())
            {
                return quieryBuilder
                    .AddPart(QueryConstants.CreateInsertQuery)
                    .AddSeparator(" ")
                    .AddPart(_connectionString.DbScheme)
                    .AddSeparator(".")
                    .AddPart(dynamicEntity.EntityName)
                    .AddSeparator(" ")
                    .AddPart($"({properties.GetPropertiesNames()})")
                    .AddPart($" VALUES ({properties.GetValuesNames()})")
                    .AddSeparator(";")
                    .GetQuery();
            }
        }
    }
}
