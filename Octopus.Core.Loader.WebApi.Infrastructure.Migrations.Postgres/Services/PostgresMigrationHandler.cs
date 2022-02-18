using System;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Services
{
    public class PostgresMigrationHandler : IProviderMigration
    {
        private readonly IPostgresMigrationService _migrationService;

        public PostgresMigrationHandler(IPostgresMigrationService migrationService)
        {
            _migrationService = migrationService;
        }

        public void InvalidSchemaNameHandler()
        {
            _migrationService.CreateSchemeAsync();
        }

        public void NotNullViolationHandler()
        {
            throw new NotImplementedException();
        }

        public void UndefinedColumnHandler()
        {
            throw new NotImplementedException();
        }

        public void UndefinedTableHandler(DynamicEntityWithProperties dynamicEntity)
        {
            _migrationService.CreateTableAsync(dynamicEntity);
        }

        public void UniqueViolationNameHandler(DynamicEntityWithProperties dynamicEntity)
        {
            _migrationService.TableCheckAsync(dynamicEntity);
        }
    }
}
