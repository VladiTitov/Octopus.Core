using Microsoft.Extensions.DependencyInjection;
using Octopus.Core.Common.DynamicObject.Services;
using Octopus.Core.Common.DynamicObject.Services.Interfaces;
using Octopus.Core.Common.Helpers.JsonDeserializer;
using Octopus.Core.Parser.BusinessLogic.Interfaces.Services;
using Octopus.Core.Parser.BusinessLogic.Services;

namespace Octopus.Core.Parser.Api.Configuration.Ioc
{
    public static class BusinessLogicServicesConfiguration
    {
        public static IServiceCollection RegisterBusinessLogicServices(this IServiceCollection services)
            => services
                .AddSingleton<IParserProcessor, ParserProcessor>()
                .AddSingleton<IDynamicObjectCreateService, DynamicObjectCreateService>()
                .AddSingleton<IJsonDeserializer, JsonDeserializer>()
                .AddSingleton<IDynamicTypeFactory, DynamicTypeFactory>();
    }
}
