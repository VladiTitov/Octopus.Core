using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Octopus.Core.Loader.WebApi.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void AddJsonFileExtension(this IConfigurationBuilder configurationBuilder, HostBuilderContext hostContext)
        {
            var baseConfigPath = $"Configs/{ hostContext.HostingEnvironment.EnvironmentName}";

            configurationBuilder.AddJsonFile($"{baseConfigPath}/DbConnectionString.json", optional: true);
            configurationBuilder.AddJsonFile($"{baseConfigPath}/MongoDbConnectionString.json", optional: true);
            configurationBuilder.AddJsonFile($"{baseConfigPath}/RabbitMqConfigure.json", optional: true);
            configurationBuilder.AddJsonFile($"{baseConfigPath}/RabbitMqConnection.json", optional: true);
        }
    }
}
