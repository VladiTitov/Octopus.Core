using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Octopus.Core.Loader.WebApi.Configuration.AppSettings;
using Octopus.Core.Loader.WebApi.Configuration.Swagger;

namespace Octopus.Core.Loader.WebApi.Configuration.Ioc
{
    public static class RootConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AppSettingsSectionsRegister(configuration)
                .RegisterDynamicEntityServices()
                .RegisterDataBaseServices()
                .RegisterRabbitMqServices()
                .RegisterMongoDbServices()
                .RegisterHelpersServices()
                .RegisterSwagger();
    }
}
