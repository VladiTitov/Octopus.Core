namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Models
{
    public class DatabaseAndSystemGeneralType
    {
        public string SystemTypeName { get; }
        public string DatabaseTypeName { get; }

        public DatabaseAndSystemGeneralType(string systemTypeName, string databaseTypeName)
        {
            SystemTypeName = systemTypeName;
            DatabaseTypeName = databaseTypeName;
        }
    }
}
