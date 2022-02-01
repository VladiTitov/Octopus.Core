using Octopus.Core.Common.ConfigsModels.Rabbit;
using Octopus.Core.RabbitMq.Context;
using RabbitMQ.Client;

namespace Octopus.Core.RabbitMq.Services
{
    public class RabbitMqInitializer
    {
        public IConnection Connection { get; set; }

        public RabbitMqInitializer(IRabbitMqContext context)
        {
            if (Connection == null) Connection = context.Connection;
        }

        public IModel InitializeRabbitMqChannel(RabbitMqConfiguration configuration)
        {
            var channel = Connection.CreateModel();

            channel.ExchangeDeclare(exchange: configuration.ExchangeName, type: configuration.ExchangeType);
            channel.QueueDeclare(queue: configuration.QueueName);
            channel.QueueBind(queue: configuration.QueueName, exchange: configuration.ExchangeName, routingKey: configuration.RoutingKey);

            return channel;
        }

        public void Dispose()
        {
            if (Connection.IsOpen)
            {
                Connection.Close();
            }
        }
    }
}
