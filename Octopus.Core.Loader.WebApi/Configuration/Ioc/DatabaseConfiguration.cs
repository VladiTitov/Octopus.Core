using Microsoft.Extensions.DependencyInjection;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.DatabaseContext;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Models;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services;

namespace Octopus.Core.Loader.WebApi.Configuration.Ioc
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection RegisterDataBaseServices(this IServiceCollection services)
            => services
                .AddScoped<IDatabaseProvidersFactory, DatabaseProvidersFactory>()
                .AddScoped<IMigrationCreateService, MigrationCreateService>()
                .AddScoped<IQueryHandlerService, QueryHandlerService>()
                .AddScoped<IDatabaseContext, DapperDbContext>()
                .AddScoped<IQueryFactoryService, QueryFactoryService>()
                .AddScoped<CreateSchemeQueryModel>()
                .AddScoped<CreateTableQueryModel>()
                .AddScoped<CreateCommentQueryModel>()
                .AddScoped<InsertQueryModel>();
    }
}
