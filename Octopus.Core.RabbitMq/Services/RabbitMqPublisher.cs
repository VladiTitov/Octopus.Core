using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Octopus.Core.Common.ConfigsModels.Rabbit.Base;
using Octopus.Core.RabbitMq.Interfaces;
using RabbitMQ.Client;

namespace Octopus.Core.RabbitMq.Services
{
    public class RabbitMqPublisher : RabbitMqInitializer, IRabbitMqPublisher
    {
        private readonly ILogger<RabbitMqPublisher> _logger;
        public readonly IModel Channel;
        public string ExchangeName;
        public string QueueName;
        public string RoutingKey;

        public RabbitMqPublisher(ILogger<RabbitMqPublisher> logger, 
            IRabbitMqContext context, 
            IOptions<PublisherConfiguration> configuration) : base(context)
        {
            _logger = logger;

            ExchangeName = configuration.Value.ExchangeName;
            QueueName = configuration.Value.QueueName;
            RoutingKey = configuration.Value.RoutingKey;

            Channel = InitializeRabbitMqChannel(configuration.Value);
            _logger.LogInformation($"Listening on the message bus. Exchange: {ExchangeName}, Queue: {QueueName}");
        }

        public Task SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            Channel.BasicPublish(exchange: ExchangeName, routingKey: RoutingKey, basicProperties: null, body: body);
            _logger.LogInformation("Message sent");
            return Task.CompletedTask;
        }
    }
}
