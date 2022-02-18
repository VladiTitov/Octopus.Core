using Microsoft.Extensions.DependencyInjection;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Services;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Services;

namespace Octopus.Core.Loader.WebApi.Configuration.Ioc
{
    public static class MigrationsConfiguration
    {
        public static IServiceCollection RegisterMigrationsServices(this IServiceCollection services)
            => services
                .AddScoped<IMigrationService, MigrationService>()
                .AddScoped<IMigrationForProvidersFactory, MigrationForProvidersFactory>()
                .AddScoped<IPostgresMigrationService, PostgresMigrationService>();
    }
}
