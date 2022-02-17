using System;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Migrations
{
    public class MigrationService : IMigrationService
    {
        private readonly IMigrationRepository _migrationRepository;

        public MigrationService(IMigrationRepository migrationRepository)
        {
            _migrationRepository = migrationRepository;
        }

        public void InvalidSchemaNameHandler()
        {
            _migrationRepository.CreateSchemeAsync();
        }

        public void UndefinedTableHandler(DynamicEntityWithProperties dynamicEntity)
        {
            _migrationRepository.CreateTableAsync(dynamicEntity);
        }

        public void UniqueViolationNameHandler(DynamicEntityWithProperties dynamicEntity)
        {
            _migrationRepository.TableCheck(dynamicEntity);
        }

        public void NotNullViolationHandler()
        {
            throw new NotImplementedException();
        }

        public void UndefinedColumnHandler()
        {
            throw new NotImplementedException();
        }
    }
}
