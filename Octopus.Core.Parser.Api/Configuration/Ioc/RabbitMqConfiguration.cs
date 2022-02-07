using Microsoft.Extensions.DependencyInjection;
using Octopus.Core.Parser.BusinessLogic.Services;
using Octopus.Core.RabbitMq.Context;
using Octopus.Core.RabbitMq.Interfaces;
using Octopus.Core.RabbitMq.Services;

namespace Octopus.Core.Parser.Api.Configuration.Ioc
{
    public static class RabbitMqConfiguration
    {
        public static IServiceCollection RegisterRabbitMq(this IServiceCollection services)
            => services
                .AddSingleton<IRabbitMqContext, RabbitMqContext>()
                .AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>()
                .AddSingleton<IRabbitMqSubscriber, RabbitMqSubscriber>()
                .AddSingleton<IEventProcessor, MessageHandler>();
    }
}
