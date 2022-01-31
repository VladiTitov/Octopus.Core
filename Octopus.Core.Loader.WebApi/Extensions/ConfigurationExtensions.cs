using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Octopus.Core.Loader.WebApi.Extensions
{
    public static class ConfigurationExtensions
    {
        [Obsolete]
        public static void AddJsonFileExtension(this IConfigurationBuilder configurationBuilder, HostBuilderContext hostContext) 
        {
            configurationBuilder.AddJsonFile($"Configs/{hostContext.HostingEnvironment.EnvironmentName}/DbConnectionString.json", optional: true);
            configurationBuilder.AddJsonFile($"Configs/{hostContext.HostingEnvironment.EnvironmentName}/RabbitMqConfigure.json", optional: true);
            configurationBuilder.AddJsonFile($"Configs/{hostContext.HostingEnvironment.EnvironmentName}/RabbitMqConnection.json", optional: true);
        }
    }
}
