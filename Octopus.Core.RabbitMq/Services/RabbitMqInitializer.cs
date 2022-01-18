using System;
using System.Collections.Generic;
using Octopus.Core.Common.ConfigsModels.Rabbit;
using Octopus.Core.RabbitMq.Context;
using RabbitMQ.Client;

namespace Octopus.Core.RabbitMq.Services
{
    public class RabbitMqInitializer : IRabbitMqInitializer
    {
        private readonly IConnection _connection;
        public readonly IModel Channel;
        public readonly string ExchangeType;
        public readonly string ExchangeName;
        public readonly string RoutingKey;
        public readonly string QueueName;

        public RabbitMqInitializer(IRabbitMqContext context, IEnumerable<RabbitMqConfiguration> configurations)
        {
            if (_connection == null) _connection = context.Connection;
            Channel = _connection.CreateModel();

            foreach (var configuration in configurations)
            {
                ExchangeType = configuration.ExchangeType;
                ExchangeName = configuration.ExchangeName;
                RoutingKey = configuration.RoutingKey;
                QueueName = configuration.QueueName;
            }
        }

        public void InitializeRabbitMq()
        {
            Channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType);
            Channel.QueueDeclare(queue: QueueName);
            Channel.QueueBind(queue: QueueName, exchange: ExchangeName, routingKey: RoutingKey);

            Console.WriteLine("Listening on the message bus");

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("RabbitMQ connection shutdown");
        }

        public void Dispose()
        {
            if (Channel.IsOpen)
            {
                Channel.Close();
                _connection.Close();
            }
        }
    }
}
