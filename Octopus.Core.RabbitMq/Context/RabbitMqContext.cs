using Microsoft.Extensions.Options;
using Octopus.Core.Common.ConfigsModels.Rabbit;
using Octopus.Core.RabbitMq.Interfaces;
using RabbitMQ.Client;

namespace Octopus.Core.RabbitMq.Context
{
    public class RabbitMqContext : IRabbitMqContext
    {
        private IConnection _connection;
        public IConnection Connection 
        {
            get { return _connection ??= CreateNewRabbitConnection(); }
            set => _connection = value;
        }

        private readonly ConnectionConfiguration _rabbitMqConfiguration;

        public RabbitMqContext(IOptions<ConnectionConfiguration> rabbitMqConfiguration)
        {
            _rabbitMqConfiguration = rabbitMqConfiguration.Value;
        }

        public IConnection CreateNewRabbitConnection()
        {
            var factory = new ConnectionFactory()
            {
                UserName = _rabbitMqConfiguration.UserName,
                Password = _rabbitMqConfiguration.Password,
                Port = _rabbitMqConfiguration.Port,
                VirtualHost = _rabbitMqConfiguration.VirtualHost,
                HostName = _rabbitMqConfiguration.Hostname
            };

            return factory.CreateConnection();
        }
    }
}
