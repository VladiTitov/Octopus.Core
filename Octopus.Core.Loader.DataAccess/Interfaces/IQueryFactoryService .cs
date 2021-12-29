namespace Octopus.Core.Loader.DataAccess.Interfaces
{
    public interface IQueryFactoryService
    {
        string GetInsertQuery(object item);
        string GetCreateTableQuery();
        string GetCreateSchemeQuery();
        string GetCreateCommentQuery();
    }
}
