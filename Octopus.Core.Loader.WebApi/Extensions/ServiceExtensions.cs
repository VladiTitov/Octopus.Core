using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Common.ConfigsModels.Rabbit;
using Octopus.Core.Common.ConfigsModels.Rabbit.Base;
using System.Collections.Generic;

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
            services.Configure<RabbitMqConfiguration>(hostContext.Configuration.GetSection("RabbitParams"));
            services.Configure<ConnectionStringConfig>(hostContext.Configuration.GetSection("ConnectionString"));
            services.Configure<ConnectionConfiguration>(hostContext.Configuration.GetSection("RabbitMqConnectionString"));
            services.Configure<PublisherConfiguration>(hostContext.Configuration.GetSection("Publisher"));

            services.AddSingleton(hostContext.Configuration.GetSection("Subscribers").Get<IEnumerable<SubscriberConfiguration>>());
        }
    }
}
