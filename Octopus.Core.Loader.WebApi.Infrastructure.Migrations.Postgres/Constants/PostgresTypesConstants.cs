using System.Collections.Generic;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Models;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Constants
{
    public static class PostgresTypesConstants
    {
        public static IEnumerable<DatabaseAndSystemGeneralType> PostgresRegisteredTypes 
            = new List<DatabaseAndSystemGeneralType>
        {
            new DatabaseAndSystemGeneralType(systemTypeName: "System.Int16", databaseTypeName: "smallint"),
            new DatabaseAndSystemGeneralType(systemTypeName: "System.Int32", databaseTypeName:"integer"),
            new DatabaseAndSystemGeneralType(systemTypeName: "System.Int64", databaseTypeName:"bigint"),
            new DatabaseAndSystemGeneralType(systemTypeName: "System.Decimal", databaseTypeName:"numeric"),
            new DatabaseAndSystemGeneralType(systemTypeName: "System.Single", databaseTypeName:"real"),
            new DatabaseAndSystemGeneralType(systemTypeName: "System.Double", databaseTypeName:"double precision"),
            new DatabaseAndSystemGeneralType(systemTypeName: "System.String", databaseTypeName:"text"),
            new DatabaseAndSystemGeneralType(systemTypeName: "DateTime", databaseTypeName: "timestamp")
        };
    }
}
