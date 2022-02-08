using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Octopus.Core.Common.ApiConfigs;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Common.ConfigsModels.Rabbit;
using Octopus.Core.Common.ConfigsModels.Rabbit.Base;
using Octopus.Core.Common.Constants;
using Octopus.Core.Parser.WorkerService.Configs.Implementations;
using Octopus.Core.Parser.WorkerService.Configuration.Implementations;
using System.Collections.Generic;

namespace Octopus.Core.Parser.Api.Configuration.AppSettings
{
    public static class AppSettingsRegistration
    {
        public static IServiceCollection RegisterAppSettingsSections(this IServiceCollection services,
            IConfiguration configuration)
            => services
                .Configure<ApplicationConfiguration>(configuration.GetSection(ConfigurationSectionNames.Application))
                .Configure<ProcessorConfiguration>(configuration.GetSection(nameof(ProcessorConfiguration)))
                .Configure<ConnectionConfiguration>(configuration.GetSection(ConfigurationSectionNames.RabbitConnection))
                .Configure<PublisherConfiguration>(configuration.GetSection(ConfigurationSectionNames.Publisher))
                .Configure<CsvParserConfiguration>(configuration.GetSection(nameof(CsvParserConfiguration)))
                .Configure<JsonParserConfiguration>(configuration.GetSection(nameof(JsonParserConfiguration)))
                .Configure<XmlParserConfiguration>(configuration.GetSection(nameof(XmlParserConfiguration)))
                .AddSingleton(configuration.GetSection(ConfigurationSectionNames.Subscribers)
                    .Get<IEnumerable<SubscriberConfiguration>>())
                .Configure<MongoDatabaseConfiguration>(configuration.GetSection("MongoDatabaseParams"));
    }
}
