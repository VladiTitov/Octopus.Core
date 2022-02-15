using Octopus.Core.Common.Constants;
using Octopus.Core.Common.Extensions;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Constants;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Extensions;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Models
{
    public class CreateTableQueryModel : IQueryModel
    {
        private readonly ConnectionStringConfig _connectionString;
        private readonly DynamicEntityWithProperties _dynamicEntity;

        public CreateTableQueryModel(ConnectionStringConfig connectionString,
            DynamicEntityWithProperties dynamicEntity)
        {
            _connectionString = connectionString;
            _dynamicEntity = dynamicEntity;
        }

        public string GetQuery()
        {
            var propertiesTable = _dynamicEntity.Properties
                .GetPropertiesNames()
                .ToQuery(",\n");

            using (var queryBuilder = new QueryBuilderService())
            {
                return queryBuilder
                    .AddPart(QueryConstants.CreateTable)
                    .AddSeparator(" ")
                    .AddPart(QueryConstants.IfNotExists)
                    .AddSeparator(" ")
                    .AddPart(_connectionString.DbScheme)
                    .AddSeparator(".")
                    .AddPart(_dynamicEntity.EntityName)
                    .AddSeparator(" ")
                    .AddPart($"({propertiesTable});")
                    .GetQuery();
            }     
        }
    }
}
