using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Common.ConfigsModels.Rabbit;
using Octopus.Core.Common.ConfigsModels.Rabbit.Base;

namespace Octopus.Core.Loader.WebApi.Configuration.AppSettings
{
    public static class AppSettingsRegistration
    {
        public static IServiceCollection AppSettingsSectionsRegister(this IServiceCollection services, IConfiguration configuration)
        => services
            .Configure<RabbitMqConfiguration>(configuration.GetSection("RabbitParams"))
            .Configure<ConnectionStringConfig>(configuration.GetSection("ConnectionString"))
            .Configure<ConnectionConfiguration>(configuration.GetSection("RabbitMqConnectionString"))
            .Configure<PublisherConfiguration>(configuration.GetSection("Publisher"))
            .Configure<MongoDatabaseConfiguration>(configuration.GetSection("MongoDatabaseParams"))
            .AddSingleton(configuration.GetSection("Subscribers")
                .Get<IEnumerable<SubscriberConfiguration>>());
    }
}
