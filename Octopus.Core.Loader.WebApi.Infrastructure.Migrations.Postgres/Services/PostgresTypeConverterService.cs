using System;
using System.Linq;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Constants;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Services
{
    public class PostgresTypeConverterService : IPostgresTypeConverterService
    {
        public string GetSystemType(string dbType)
        {
            var registeredTypes = PostgresTypesConstants.PostgresRegisteredTypes.ToList();

            try
            {
                if (registeredTypes.Any(item => item.SystemTypeName.Equals(dbType))) return dbType;
                return registeredTypes
                    .First(i => i.DatabaseTypeName.Equals(dbType))
                    .SystemTypeName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} - \"{dbType}\"");
                throw;
                //throw new UnregisteredTypeDetected($"{ex.Message} - \"{dbType}\"");
            }
        }

        public string GetDbProviderType(string systemType)
        {
            try
            {
                return PostgresTypesConstants.PostgresRegisteredTypes
                    .First(i => i.SystemTypeName.Equals(systemType))
                    .DatabaseTypeName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} - \"{systemType}\"");
                throw;
                //throw new UnregisteredTypeDetected($"{ex.Message} - \"{dbType}\"");
            }
        }
    }
}
