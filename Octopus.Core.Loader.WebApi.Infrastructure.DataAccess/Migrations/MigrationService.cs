using System;
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
            throw new NotImplementedException();
        }

        public void UndefinedTableHandler()
        {
            throw new NotImplementedException();
        }

        public void UniqueViolationNameHandler()
        {
            throw new NotImplementedException();
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
