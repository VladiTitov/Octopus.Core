using Octopus.Core.Common.DynamicObject.Models;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Interfaces
{
    public interface IMigrationService
    {
        IProviderMigration GetMigrationForProvider();
    }
}
