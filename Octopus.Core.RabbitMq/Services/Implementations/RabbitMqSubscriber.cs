using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Octopus.Core.Common.ConfigsModels.Rabbit.Base;
using Octopus.Core.RabbitMq.Context;
using Octopus.Core.RabbitMq.Services.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Octopus.Core.RabbitMq.Services.Implementations
{
    public class RabbitMqSubscriber : RabbitMqInitializer, IRabbitMqSubscriber, IDisposable
    {
        private readonly IEventProcessor _eventProcessor;

        public RabbitMqSubscriber(IRabbitMqContext context,
            IEnumerable<SubscriberConfiguration> configurations,
            IEventProcessor eventProcessor) : base(context, configurations)
        {
            _eventProcessor = eventProcessor;
            InitializeRabbitMq();
        }

        public Task Execute()
        {
            var consumer = new EventingBasicConsumer(Channel);

            consumer.Received += (moduleHandle, e) =>
            {
                Console.WriteLine("Event Received");

                var body = e.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());

                _eventProcessor.ProcessEvent(message);
            };

            Channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
