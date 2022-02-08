using Microsoft.Extensions.DependencyInjection;
using Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Repositories;

namespace Octopus.Core.Parser.Api.Configuration.Ioc
{
    public static class DatabaseServiceConfiguration
    {
        public static IServiceCollection RegisterMongoDb(this IServiceCollection services)
            => services.AddSingleton<IMongoRepository, MongoRepository>();
    }
}
