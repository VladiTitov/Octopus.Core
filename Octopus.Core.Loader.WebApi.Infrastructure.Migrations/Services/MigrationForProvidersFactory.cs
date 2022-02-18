using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Services;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Services
{
    public class MigrationForProvidersFactory : IMigrationForProvidersFactory
    {
        private readonly IPostgresMigrationService _postgresMigrationService;

        public MigrationForProvidersFactory(IPostgresMigrationService postgresMigrationService)
        {
            _postgresMigrationService = postgresMigrationService;
        }

        public IProviderMigration GetPostgresProviderMigration() => new PostgresMigrationHandler(_postgresMigrationService);
    }
}
