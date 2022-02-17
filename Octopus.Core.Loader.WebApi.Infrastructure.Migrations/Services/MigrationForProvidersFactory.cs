using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.ProvidersMigrations;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Services
{
    public class MigrationForProvidersFactory : IMigrationForProvidersFactory
    {
        private readonly IPostgresMigrationService _postgresMigrationService;

        public MigrationForProvidersFactory(IPostgresMigrationService postgresMigrationService)
        {
            _postgresMigrationService = postgresMigrationService;
        }

        public IProviderMigration GetPostgresProviderMigration() => new PostgresMigration(_postgresMigrationService);
    }
}
