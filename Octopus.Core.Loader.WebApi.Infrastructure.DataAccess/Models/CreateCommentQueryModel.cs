using System.Linq;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Constants;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Services;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Models
{
    public class CreateCommentQueryModel : IQueryModel
    {
        private readonly ConnectionStringConfig _connectionString;
        private readonly DynamicEntityWithProperties _dynamicEntity;

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
                            .AddPart(QueryConstants.CommentOnColumn)
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
