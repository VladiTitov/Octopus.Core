using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Octopus.Core.Parser.Api.Configuration.AppSettings;
using Octopus.Core.Parser.Api.Configuration.Swagger;

namespace Octopus.Core.Parser.Api.Configuration.Ioc
{
    public static class RootConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment environment)
            => services
                .RegisterAppSettingsSections(configuration)
                .RegisterSwagger(configuration);
    }
}
