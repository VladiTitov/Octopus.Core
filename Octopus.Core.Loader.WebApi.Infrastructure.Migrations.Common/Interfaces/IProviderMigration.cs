using Octopus.Core.Common.DynamicObject.Models;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Interfaces
{
    public interface IProviderMigration
    {
        void UndefinedColumnHandler();
        void NotNullViolationHandler();
        void InvalidSchemaNameHandler();
        void UndefinedTableHandler(DynamicEntityWithProperties dynamicEntity);
        void UniqueViolationNameHandler(DynamicEntityWithProperties dynamicEntity);
    }
}
