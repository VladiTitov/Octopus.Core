namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces
{
    public interface IMigrationService
    {
        void InvalidSchemaNameHandler();
        void UndefinedTableHandler();
        void UniqueViolationNameHandler();
        void NotNullViolationHandler();
        void UndefinedColumnHandler();
    }
}
