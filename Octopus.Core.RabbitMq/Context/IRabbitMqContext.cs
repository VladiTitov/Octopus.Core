using RabbitMQ.Client;

namespace Octopus.Core.RabbitMq.Context
{
    public interface IRabbitMqContext
    {
        IConnection Connection { get; }
        IConnection GetRabbitConnection();
    }
}
