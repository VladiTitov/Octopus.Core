using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Constants;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Services;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Models
{
    public class CreateSchemeQueryModel : IQueryModel
    {
        private readonly ConnectionStringConfig _connectionString;

        public CreateSchemeQueryModel(ConnectionStringConfig connectionString)
        {
            _connectionString = connectionString;
        }

        public string GetQuery()
        {
            using (var queryBuilder = new QueryBuilderService())
            {
                return queryBuilder
                    .AddPart(QueryConstants.Create)
                    .AddPart(QueryConstants.Schema)
                    .AddSeparator(" ")
                    .AddPart(QueryConstants.IfNotExists)
                    .AddSeparator(" ")
                    .AddPart(_connectionString.DbScheme)
                    .AddSeparator(";")
                    .GetQuery();
            }
        }
    }
}