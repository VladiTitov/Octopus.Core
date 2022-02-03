using RabbitMQ.Client;

namespace Octopus.Core.RabbitMq.Interfaces
{
    public interface IRabbitMqContext
    {
        IConnection Connection { get; set; }

        IConnection CreateNewRabbitConnection();
    }
}
