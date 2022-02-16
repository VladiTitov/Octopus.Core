using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Constants;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Models
{
    public class ExistsTableQueryModel
    {
        public string GetQuery(string table, string column, string value)
        {
            using (var queryBuilder = new QueryBuilderService())
            {
                return queryBuilder
                    .AddPart(QueryConstants.Select)
                    .AddPart(QueryConstants.Exists)
                    .AddSeparator("(")
                    .AddPart(QueryConstants.Select)
                    .AddSeparator(" * ")
                    .AddPart(QueryConstants.From)
                    .AddPart(table)
                    .AddPart(QueryConstants.Where)
                    .AddPart(column)
                    .AddPart(" = ")
                    .AddSeparator("'")
                    .AddPart(value)
                    .AddSeparator("');")
                    .GetQuery();
            }
        }
    }
}
