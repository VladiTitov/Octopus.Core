using System.Text;
using Microsoft.Extensions.Options;
using Octopus.Core.Common.Configs;
using Octopus.Core.RabbitMq.Context;
using Octopus.Core.RabbitMq.Services.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Octopus.Core.RabbitMq.Services.Implementations
{
    public class RabbitMqListener : IRabbitMqListener
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName;

        public RabbitMqListener(IRabbitMqContext context, IOptions<RabbitMqConfiguration> configuration)
        {
            _connection = context.Connection;
            _channel = _connection.CreateModel();
            _queueName = configuration.Value.QueueName;
        }

        public void ChannelConsume()
        {
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            _channel.BasicConsume(queue: _queueName, autoAck: true, GetConsumer());
        }


        public EventingBasicConsumer GetConsumer()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
            };
            return consumer;
        }
    }
}
