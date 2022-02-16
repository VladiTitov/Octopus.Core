using Octopus.Core.Common.DynamicObject.Models;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces
{
    public interface IQueryFactoryService
    {
        string GetCreateSchemeQuery();
        string GetInsertQuery(DynamicEntityWithProperties dynamicEntity);
        string GetCreateTableQuery(DynamicEntityWithProperties dynamicEntity);
        string GetCreateCommentQuery(DynamicEntityWithProperties dynamicEntity);
        string GetExistsTableQuery(string table, string column, string value);
    }
}
