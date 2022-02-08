using Microsoft.Extensions.DependencyInjection;
using Octopus.Core.Common.Helpers.JsonDeserializer;
using Octopus.Core.Loader.WebApi.Core.Application.Interfaces;
using Octopus.Core.Loader.WebApi.Core.Application.Services;

namespace Octopus.Core.Loader.WebApi.Configuration.Ioc
{
    public static class HelpersConfiguration
    {
        public static IServiceCollection RegisterHelpersServices(this IServiceCollection services)
            => services
                .AddSingleton<IDataReaderService, DataReaderService>()
                .AddSingleton<IJsonDeserializer, JsonDeserializer>();
    }
}
