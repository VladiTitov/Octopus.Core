using Microsoft.Extensions.Options;
using Octopus.Core.Common.ConfigsModels.Rabbit.Base;
using RabbitMQ.Client;

namespace Octopus.Core.RabbitMq.Context
{
    public class RabbitMqContext : IRabbitMqContext
    {
        private readonly SubscriberConfiguration _subscriberConfiguration;
        private readonly PublisherConfiguration _publisherConfiguration;

        public IConnection PublisherConnection { get; }
        public IConnection SubscriberConnection { get; }

        public RabbitMqContext(IOptions<SubscriberConfiguration> subscriberConfiguration, IOptions<PublisherConfiguration> publisherConfiguration)
        {
            _subscriberConfiguration = subscriberConfiguration.Value;
            _publisherConfiguration = publisherConfiguration.Value;

            PublisherConnection = CreateNewRabbitConnection(_publisherConfiguration.UserName, _publisherConfiguration.Password, _publisherConfiguration.Port, _publisherConfiguration.VirtualHost, _publisherConfiguration.Hostname);
            SubscriberConnection = CreateNewRabbitConnection(_subscriberConfiguration.UserName, _subscriberConfiguration.Password, _subscriberConfiguration.Port, _subscriberConfiguration.VirtualHost, _subscriberConfiguration.Hostname);
        }

        public IConnection CreateNewRabbitConnection(string userName, string password, int port, string virtualHost, string hostName)
        {
            var factory = new ConnectionFactory()
            {
                UserName = userName,
                Password = password,
                Port = port,
                VirtualHost = virtualHost,
                HostName = hostName
            };

            return factory.CreateConnection();
        }
    }
}
