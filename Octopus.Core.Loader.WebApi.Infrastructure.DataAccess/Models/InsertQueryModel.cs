using System.Linq;
using Octopus.Core.Common.Extensions;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Constants;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Services;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Models
{
    public class InsertQueryModel : IQueryModel
    {
        private readonly ConnectionStringConfig _connectionString;
        private readonly DynamicEntityWithProperties _dynamicEntity;

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

            using (var queryBuilder = new QueryBuilderService())
            {
                return queryBuilder
                        .AddPart(QueryConstants.InsertInto)
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
