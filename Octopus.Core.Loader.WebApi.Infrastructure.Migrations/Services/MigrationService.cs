using Microsoft.Extensions.Options;
using Octopus.Core.Common.Constants;
using Octopus.Core.Common.Exceptions;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Constants;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Interfaces;


namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Services
{
    public class MigrationService : IMigrationService
    {
        private readonly IMigrationForProvidersFactory _migrationForProvidersFactory;
        private readonly ConnectionStringConfig _connectionString;

        public MigrationService(IOptions<ConnectionStringConfig> connectionString,
            IMigrationForProvidersFactory migrationForProvidersFactory)
        {
            _connectionString = connectionString.Value;
            _migrationForProvidersFactory = migrationForProvidersFactory;
        }

        public IProviderMigration GetMigrationForProvider()
        {
            return _connectionString.DbType switch
            {
                ProvidersNamesConstants.PostgreSql => _migrationForProvidersFactory.GetPostgresProviderMigration(),
                _ => throw new DatabaseProviderException($"{ErrorMessages.DatabaseProviderException} {_connectionString.DbType}")
            };
        }
    }
}
