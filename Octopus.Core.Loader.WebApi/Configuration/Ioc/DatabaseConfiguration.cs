using Microsoft.Extensions.DependencyInjection;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.DatabaseContext;

namespace Octopus.Core.Loader.WebApi.Configuration.Ioc
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection RegisterDataBaseServices(this IServiceCollection services)
            => services
                .AddScoped<IDatabaseExceptionFactory, DatabaseExceptionFactory>()
                .AddScoped<IDatabaseProvidersFactory, DatabaseProvidersFactory>()
                .AddScoped<IQueryHandlerService, QueryHandlerService>()
                .AddScoped<IQueryFactoryService, QueryFactoryService>()
                .AddScoped<IDatabaseContext, DapperDbContext>();
    }
}
