using Octopus.Core.Common.ConfigsModels.Rabbit;
using Octopus.Core.RabbitMq.Interfaces;
using RabbitMQ.Client;

namespace Octopus.Core.RabbitMq.Services
{
    public class RabbitMqInitializer
    {
        private readonly IRabbitMqContext _context;

        public RabbitMqInitializer(IRabbitMqContext context)
        {
            _context = context;
        }

        public IModel InitializeRabbitMqChannel(RabbitMqConfiguration configuration)
        {
            var channel = _context.Connection.CreateModel();

            channel.ExchangeDeclare(exchange: configuration.ExchangeName, type: configuration.ExchangeType);
            channel.QueueDeclare(queue: configuration.QueueName, durable: true, exclusive: false, autoDelete: true);
            channel.QueueBind(queue: configuration.QueueName, exchange: configuration.ExchangeName, routingKey: configuration.RoutingKey);

            return channel;
        }

        public void Dispose()
        {
            if (!_context.Connection.IsOpen) return;
            _context.Connection.Close();
            _context.Connection = null;
        }
    }
}
