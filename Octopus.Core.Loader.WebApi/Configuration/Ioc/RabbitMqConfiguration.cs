using Microsoft.Extensions.DependencyInjection;
using Octopus.Core.Loader.WebApi.Core.Application.Services;
using Octopus.Core.RabbitMq.Context;
using Octopus.Core.RabbitMq.Interfaces;
using Octopus.Core.RabbitMq.Services;

namespace Octopus.Core.Loader.WebApi.Configuration.Ioc
{
    public static class RabbitMqConfiguration
    {
        public static IServiceCollection RegisterRabbitMqServices(this IServiceCollection services)
            => services
                .AddSingleton<IRabbitMqContext, RabbitMqContext>()
                .AddSingleton<IRabbitMqSubscriber, RabbitMqSubscriber>()
                .AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>()
                .AddSingleton<IEventProcessor, MessageHandler>();
    }
}
