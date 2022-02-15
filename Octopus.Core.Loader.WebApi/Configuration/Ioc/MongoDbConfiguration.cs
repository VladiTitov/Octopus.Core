using Microsoft.Extensions.DependencyInjection;
using Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Context;
using Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Repositories;

namespace Octopus.Core.Loader.WebApi.Configuration.Ioc
{
    public static class MongoDbConfiguration
    {
        public static IServiceCollection RegisterMongoDbServices(this IServiceCollection services)
            => services
                .AddScoped<IMongoContext, MongoContext>()
                .AddScoped<IMongoRepository, MongoRepository>();
    }
}
