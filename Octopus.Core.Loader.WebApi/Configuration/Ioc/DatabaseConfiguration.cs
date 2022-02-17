using Microsoft.Extensions.DependencyInjection;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.DatabaseContext;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Services;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.ProvidersMigrations;

namespace Octopus.Core.Loader.WebApi.Configuration.Ioc
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection RegisterDataBaseServices(this IServiceCollection services)
            => services
                .AddScoped<IDatabaseExceptionFactory, DatabaseExceptionFactory>()
                .AddScoped<IDatabaseProvidersFactory, DatabaseProvidersFactory>()
                .AddScoped<IMigrationService, MigrationService>()
                .AddScoped<IMigrationForProvidersFactory, MigrationForProvidersFactory>()
                .AddScoped<IPostgresMigrationService, PostgresMigrationService>()
                .AddScoped<IQueryHandlerService, QueryHandlerService>()
                .AddScoped<IQueryFactoryService, QueryFactoryService>()
                .AddScoped<IDatabaseContext, DapperDbContext>();
    }
}
