namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Interfaces
{
    public interface IPostgresTypeConverterService
    {
        string GetSystemType(string dbType);
        string GetDbProviderType(string systemType);
    }
}
