using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Constants;

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
                    .AddPart(QueryConstants.CreateSchema)
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