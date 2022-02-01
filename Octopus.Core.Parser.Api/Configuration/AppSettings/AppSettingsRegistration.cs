using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Octopus.Core.Common.ApiConfigs;
using Octopus.Core.Common.Constants;

namespace Octopus.Core.Parser.Api.Configuration.AppSettings
{
    public static class AppSettingsRegistration
    {
        public static IServiceCollection RegisterAppSettingsSections(this IServiceCollection services,
            IConfiguration configuration)
            => services
                .Configure<ApplicationConfiguration>(configuration.GetSection(ConfigurationSectionNames.Application));
    }
}
