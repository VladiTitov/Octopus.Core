using Octopus.Core.Common.DynamicObject.Models;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Interfaces
{
    public interface IProviderMigration
    {
        void UndefinedColumnHandler();
        void InvalidSchemaNameHandler();
        void NotNullViolationHandler(DynamicEntityWithProperties dynamicEntity);
        void UndefinedTableHandler(DynamicEntityWithProperties dynamicEntity);
        void UniqueViolationNameHandler(DynamicEntityWithProperties dynamicEntity);
    }
}
