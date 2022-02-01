using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Common.ConfigsModels.Rabbit;
using Octopus.Core.Common.ConfigsModels.Rabbit.Base;
using System.Collections.Generic;
using Octopus.Core.Common.DynamicObject.Services;
using Octopus.Core.Common.DynamicObject.Services.Interfaces;
using Octopus.Core.Common.Helpers.JsonDeserializer;
using Octopus.Core.Loader.WebApi.Core.Application.Interfaces;
using Octopus.Core.Loader.WebApi.Core.Application.Services;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.DatabaseContext;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Repositories;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services;

namespace Octopus.Core.Loader.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen();
        }

        public static void ConfigureExtension(this IServiceCollection services, HostBuilderContext hostContext) 
        {
            services.Configure<RabbitMqConfiguration>(hostContext.Configuration.GetSection("RabbitParams"))
                .Configure<ConnectionStringConfig>(hostContext.Configuration.GetSection("ConnectionString"))
                .Configure<ConnectionConfiguration>(hostContext.Configuration.GetSection("RabbitMqConnectionString"))
                .Configure<PublisherConfiguration>(hostContext.Configuration.GetSection("Publisher"));
            
        }

        public static void AddConfigurationsExtension(this IServiceCollection services, HostBuilderContext hostContext) =>
            services
                .AddSingleton(hostContext.Configuration.GetSection("Subscribers")
                .Get<IEnumerable<SubscriberConfiguration>>());

        public static void AddDynamicEntityServicesExtension(this IServiceCollection services)
        {
            services.AddSingleton<IDynamicObjectCreateService, DynamicObjectCreateService>();
            services.AddSingleton<IDynamicTypeFactory, DynamicTypeFactory>();
            services.AddSingleton<IDynamicEntityService, DynamicEntityService>();
            services.AddSingleton<IDynamicEntityRepository, DynamicEntityRepository>();
        }

        public static void AddDataBaseServicesExtension(this IServiceCollection services)
        {
            services.AddSingleton<IDatabaseProvidersFactory, DatabaseProvidersFactory>();
            services.AddSingleton<IMigrationCreateService, MigrationCreateService>();
            services.AddSingleton<IQueryFactoryService, QueryFactoryService>();
            services.AddSingleton<IQueryHandlerService, QueryHandlerService>();
            services.AddSingleton<IDatabaseContext, DapperDbContext>();
        }

        public static void AddHelpersServicesExtension(this IServiceCollection services)
        {
            services.AddSingleton<IDataReaderService, DataReaderService>();
            services.AddSingleton<IJsonDeserializer, JsonDeserializer>();
        }
    }
}
