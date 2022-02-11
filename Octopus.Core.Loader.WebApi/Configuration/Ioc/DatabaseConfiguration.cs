using Microsoft.Extensions.DependencyInjection;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.DatabaseContext;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services;

namespace Octopus.Core.Loader.WebApi.Configuration.Ioc
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection RegisterDataBaseServices(this IServiceCollection services)
            => services
                .AddSingleton<IDatabaseProvidersFactory, DatabaseProvidersFactory>()
                .AddSingleton<IMigrationCreateService, MigrationCreateService>()
                .AddSingleton<IQueryHandlerService, QueryHandlerService>()
                .AddSingleton<IDatabaseContext, DapperDbContext>()
                .AddSingleton<IQueryFactoryService, QueryFactoryService>();
    }
}
