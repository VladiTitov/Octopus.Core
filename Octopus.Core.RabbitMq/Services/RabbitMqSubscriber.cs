using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Octopus.Core.Common.ConfigsModels.Rabbit.Base;
using Octopus.Core.RabbitMq.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Octopus.Core.RabbitMq.Services
{
    public class RabbitMqSubscriber : RabbitMqInitializer, IRabbitMqSubscriber
    {
        private readonly IEventProcessor _eventProcessor;
        private readonly ILogger<RabbitMqSubscriber> _logger;

        public RabbitMqSubscriber(ILogger<RabbitMqSubscriber> logger, 
            IRabbitMqContext context, 
            IEnumerable<SubscriberConfiguration> configurations, 
            IEventProcessor eventProcessor) : base(context)
        {
            _logger = logger;
            _eventProcessor = eventProcessor;
        }

        private Task Execute(IModel channel, string queueName)
        {
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (moduleHandle, e) =>
            {
                _logger.LogInformation($"Event Received. Queue: {queueName}");

                var body = e.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());

                _eventProcessor.ProcessEvent(message);
            };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        public async void StartService(IEnumerable<SubscriberConfiguration> configurations)
        {
            foreach (var configuration in configurations)
            {
                var channel = InitializeRabbitMqChannel(configuration);
                await Execute(channel, configuration.QueueName);
                _logger.LogInformation($"Listening on the message bus. Exchange: {configuration.ExchangeName}, Queue: {configuration.QueueName}");
            }
        }

        public void StopService()
        {
            _logger.LogInformation($"Subscriber service stopping at: {DateTime.Now}");
            base.Dispose();
        }
    }
}
