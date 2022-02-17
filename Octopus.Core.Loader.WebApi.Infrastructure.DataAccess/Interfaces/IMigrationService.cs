using Octopus.Core.Common.DynamicObject.Models;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces
{
    public interface IMigrationService
    {
        void InvalidSchemaNameHandler();
        void UndefinedTableHandler(DynamicEntityWithProperties dynamicEntity);
        void UniqueViolationNameHandler(DynamicEntityWithProperties dynamicEntity);
        void NotNullViolationHandler();
        void UndefinedColumnHandler();
    }
}
