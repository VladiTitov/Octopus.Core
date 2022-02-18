using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Services;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Interfaces
{
    public interface IQueryBuilderService
    {
        string GetQuery();
        QueryBuilderService AddPart(string queryPart);
        QueryBuilderService AddSeparator(string separator);
    }
}
