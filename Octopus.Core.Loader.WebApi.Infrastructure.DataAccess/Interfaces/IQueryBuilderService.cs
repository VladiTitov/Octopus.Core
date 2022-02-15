using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces
{
    public interface IQueryBuilderService
    {
        string GetQuery();
        QueryBuilderService AddPart(string queryPart);
        QueryBuilderService AddSeparator(string separator);
    }
}
