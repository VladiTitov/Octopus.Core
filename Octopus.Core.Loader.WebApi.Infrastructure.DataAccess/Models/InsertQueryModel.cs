using System.Linq;
using Octopus.Core.Common.Extensions;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Constants;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Models
{
    public class InsertQueryModel : IQueryModel
    {
        private ConnectionStringConfig _connectionString;
        private DynamicEntityWithProperties _dynamicEntity;

        public InsertQueryModel(ConnectionStringConfig connectionString,
            DynamicEntityWithProperties dynamicEntity)
        {
            _connectionString = connectionString;
            _dynamicEntity = dynamicEntity;
        }

        public string GetQuery()
        {
            var properties = _dynamicEntity
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
                        .AddPart(_dynamicEntity.EntityName)
                        .AddSeparator(" ")
                        .AddPart($"({properties.GetPropertiesNames()})")
                        .AddPart($" {QueryConstants.Values} ")
                        .AddPart($"({properties.GetValuesNames()})")
                        .AddSeparator(";")
                    .GetQuery();
            }
        }
    }
}
