using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Octopus.Core.Common.Configs;
using Octopus.Core.RabbitMq.Context;
using Octopus.Core.RabbitMq.Services.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Octopus.Core.RabbitMq.Workers
{
    public class MessageBusSubscriber : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _exchangeType;
        private readonly string _exchangeName;
        private readonly string _routingKey;
        private readonly string _queueName;
        private readonly IEventProcessor _eventProcessor;

        private readonly ILogger<MessageBusSubscriber> _logger;

        public MessageBusSubscriber(ILogger<MessageBusSubscriber> logger, 
            IRabbitMqContext context,
            IOptions<RabbitMqConfiguration> configuration,
            IEventProcessor eventProcessor)
        {
            _logger = logger;

            _connection = context.Connection;
            _channel = _connection.CreateModel();
            _exchangeType = configuration.Value.ExchangeType;
            _exchangeName = configuration.Value.ExchangeName;
            _routingKey = configuration.Value.RoutingKey;
            _queueName = configuration.Value.QueueName;

            _eventProcessor = eventProcessor;

            InitializeRabbitMQ();
        }

        private void InitializeRabbitMQ()
        {
            _channel.ExchangeDeclare(exchange: _exchangeName, type: _exchangeType);
            _channel.QueueDeclare(queue: _queueName);
            _channel.QueueBind(queue: _queueName, exchange: _exchangeName, routingKey: _routingKey);

            _logger.LogInformation("Listening on the message bus");

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            _logger.LogWarning("RabbitMQ connection shutdown");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (ModuleHandle, e) =>
            {
                _logger.LogInformation("Event Received");

                var body = e.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());

                _eventProcessor.ProcessEvent(message);
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
            base.Dispose();
        }
    }
}
