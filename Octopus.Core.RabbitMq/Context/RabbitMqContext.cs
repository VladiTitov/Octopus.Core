using Microsoft.Extensions.Options;
using Octopus.Core.Common.ConfigsModels.Rabbit;
using RabbitMQ.Client;

namespace Octopus.Core.RabbitMq.Context
{
    public class RabbitMqContext : IRabbitMqContext
    {
        public IConnection Connection { get; }

        public RabbitMqContext(IOptions<ConnectionConfiguration> rabbitMqConfiguration)
        {
            Connection = CreateNewRabbitConnection(rabbitMqConfiguration.Value.UserName,
                rabbitMqConfiguration.Value.Password,
                rabbitMqConfiguration.Value.Port,
                rabbitMqConfiguration.Value.VirtualHost,
                rabbitMqConfiguration.Value.Hostname);
        }

        public IConnection CreateNewRabbitConnection(
            string userName,
            string password,
            int port,
            string virtualHost,
            string hostName)
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
