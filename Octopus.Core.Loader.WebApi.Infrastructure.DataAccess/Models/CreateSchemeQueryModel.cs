using Microsoft.Extensions.Options;
using Octopus.Core.Common.Constants;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Models
{
    public class CreateSchemeQueryModel : IQueryModel
    {
        private readonly ConnectionStringConfig _connectionString;

        public CreateSchemeQueryModel(IOptions<ConnectionStringConfig> connectionString)
        {
            _connectionString = connectionString.Value;
        }

        public string GetQuery()
        {
            using (var queryBuilder = new QueryBuilderService())
            {
                return queryBuilder
                .AddPart(QueryConstants.CreateSchemaQuery)
                .AddSeparator(" ")
                .AddPart(QueryConstants.IfNotExistsQuery)
                .AddSeparator(" ")
                .AddPart(_connectionString.DbScheme)
                .AddSeparator(";")
                .GetQuery();
            }  
        }
    }
}
