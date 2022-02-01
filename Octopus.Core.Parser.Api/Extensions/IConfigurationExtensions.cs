using Microsoft.Extensions.Configuration;
using Octopus.Core.Common.ApiConfigs;
using Octopus.Core.Common.Constants;

namespace Octopus.Core.Parser.Api.Extensions
{
    public static class IConfigurationExtensions
    {
        public static ApplicationConfiguration GetApplicationConfiguration(this IConfiguration configuration)
            => configuration.GetSection(ConfigurationSectionNames.Application).Get<ApplicationConfiguration>();
    }
}
