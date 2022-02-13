using System.Linq;
using Microsoft.Extensions.Options;
using Octopus.Core.Common.Constants;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Octopus.Core.Common.Extensions;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Models
{
    public class CreateCommentQueryModel : IQueryModelFromDynamicEntity
    {
        private ConnectionStringConfig _connectionString;
        private IQueryBuilderService _queryBuilderService;

        public CreateCommentQueryModel(IOptions<ConnectionStringConfig> connectionString)
        {
            _connectionString = connectionString.Value;
            _queryBuilderService = new QueryBuilderService();
        }

        public string GetQuery(DynamicEntityWithProperties dynamicEntity)
        {
            var commentsList = dynamicEntity
                .Properties
                .Select(property => $"{QueryConstants.CreateCommentOnColumnQuery} " +
                                    $"\"{_connectionString.DbScheme}\".\"{property.PropertyName}\" " +
                                    $"IS '{property.DynamicEntityDataBaseProperty.Comment}';").ToList();

            return commentsList.ToQuery("");
        }
    }
}
