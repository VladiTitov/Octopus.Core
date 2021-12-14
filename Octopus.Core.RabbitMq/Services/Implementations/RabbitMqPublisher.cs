using System.Text;
using Microsoft.Extensions.Options;
using Octopus.Core.Common.Configs;
using Octopus.Core.RabbitMq.Context;
using Octopus.Core.RabbitMq.Services.Interfaces;
using RabbitMQ.Client;

namespace Octopus.Core.RabbitMq.Services.Implementations
{
    public class RabbitMqPublisher : IRabbitMqPublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName;

        public RabbitMqPublisher(IRabbitMqContext context, IOptions<RabbitMqConfiguration> configuration)
        {
            _connection = context.Connection;
            _channel = _connection.CreateModel();
            _queueName = configuration.Value.QueueName;
        }

        public void ChannelConsume(string message)
        {
            _channel.QueueDeclare(queue: _queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "",
                routingKey: _queueName,
                basicProperties: null,
                body: body);
        }
    }
}
