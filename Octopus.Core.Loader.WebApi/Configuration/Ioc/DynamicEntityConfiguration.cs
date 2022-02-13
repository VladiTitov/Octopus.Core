using Microsoft.Extensions.DependencyInjection;
using Octopus.Core.Common.DynamicObject.Services;
using Octopus.Core.Common.DynamicObject.Services.Interfaces;
using Octopus.Core.Loader.WebApi.Core.Application.Interfaces;
using Octopus.Core.Loader.WebApi.Core.Application.Services;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Repositories;

namespace Octopus.Core.Loader.WebApi.Configuration.Ioc
{
    public static class DynamicEntityConfiguration
    {
        public static IServiceCollection RegisterDynamicEntityServices(this IServiceCollection services)
            => services
                .AddScoped<IDynamicObjectCreateService, DynamicObjectCreateService>()
                .AddScoped<IDynamicTypeFactory, DynamicTypeFactory>()
                .AddScoped<IDynamicEntityRepository, DynamicEntityRepository>()
                .AddScoped<IDynamicEntityService, DynamicEntityService>();
    }
}
