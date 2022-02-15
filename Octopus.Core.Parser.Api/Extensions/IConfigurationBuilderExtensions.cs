using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Octopus.Core.Parser.Api.Extensions
{
    public static class IConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddJsonFiles(this IConfigurationBuilder configurationBuilder,
            HostBuilderContext hostContext)
        {
            var baseConfigPath = $"Configs/{ hostContext.HostingEnvironment.EnvironmentName}";

            return configurationBuilder.AddJsonFile($"{baseConfigPath}/Parsers/CsvParser.json")
                .AddJsonFile($"{baseConfigPath}/Parsers/JsonParser.json")
                .AddJsonFile($"{baseConfigPath}/Parsers/XmlParser.json")
                .AddJsonFile($"{baseConfigPath}/Processor.json")
                .AddJsonFile($"{baseConfigPath}/RabbitMq.json")
                .AddJsonFile($"{baseConfigPath}/RabbitMqConnectionString.json")
                .AddJsonFile($"{baseConfigPath}/MongoDbConnectionString.json");
        }
    }
}
