using Octopus.Core.Common.DynamicObject.Models;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Interfaces
{
    public interface IProviderMigration
    {
        void InvalidSchemaNameHandler();
        void NotNullViolationHandler();
        void UndefinedColumnHandler();
        void UndefinedTableHandler(DynamicEntityWithProperties dynamicEntity);
        void UniqueViolationNameHandler(DynamicEntityWithProperties dynamicEntity);
    }
}
