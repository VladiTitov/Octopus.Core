using System.Linq;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Constants;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Models
{
    public class CreateCommentQueryModel : IQueryModel
    {
        private ConnectionStringConfig _connectionString;
        private DynamicEntityWithProperties _dynamicEntity;

        public CreateCommentQueryModel(ConnectionStringConfig connectionString,
            DynamicEntityWithProperties dynamicEntity)
        {
            _connectionString = connectionString;
            _dynamicEntity = dynamicEntity;
        }

        public string GetQuery() 
            => string
                .Join("\n", 
                    _dynamicEntity
                        .Properties
                        .Select(i => GetCommentQuery(i)));

        private string GetCommentQuery(DynamicProperty property)
        {
            using (var queryBuilder = new QueryBuilderService())
            {
                return queryBuilder
                            .AddPart(QueryConstants.CreateCommentOnColumnQuery)
                            .AddSeparator(" \"")
                            .AddPart(_connectionString.DbScheme)
                            .AddSeparator("\".\"")
                            .AddPart(property.PropertyName)
                            .AddSeparator("\" ")
                            .AddPart($"{QueryConstants.Is} ")
                            .AddPart(property.DynamicEntityDataBaseProperty.Comment)
                            .AddSeparator(";")
                        .GetQuery();
            }
        }
    }
}
