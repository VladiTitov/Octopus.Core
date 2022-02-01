namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces
{
    public interface IQueryFactoryService
    {
        string GetInsertQuery(object item);
        string GetCreateTableQuery();
        string GetCreateSchemeQuery();
        string GetCreateCommentQuery();
    }
}
