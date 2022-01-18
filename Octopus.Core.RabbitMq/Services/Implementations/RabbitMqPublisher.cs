using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Octopus.Core.Common.ConfigsModels.Rabbit.Base;
using Octopus.Core.RabbitMq.Context;
using Octopus.Core.RabbitMq.Services.Interfaces;
using RabbitMQ.Client;

namespace Octopus.Core.RabbitMq.Services.Implementations
{
    public class RabbitMqPublisher : RabbitMqInitializer, IRabbitMqPublisher
    {
        public RabbitMqPublisher(IRabbitMqContext context,
            IEnumerable<PublisherConfiguration> configurations) : base(context, configurations) { }

        public Task SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            Channel.BasicPublish(exchange: ExchangeName, routingKey: RoutingKey, basicProperties: null, body: body);

            return Task.CompletedTask;
        }

    }
}
