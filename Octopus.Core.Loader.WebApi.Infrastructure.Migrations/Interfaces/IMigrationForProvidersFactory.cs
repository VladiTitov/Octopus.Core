namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Interfaces
{
    public interface IMigrationForProvidersFactory
    {
        IProviderMigration GetPostgresProviderMigration();
    }
}
