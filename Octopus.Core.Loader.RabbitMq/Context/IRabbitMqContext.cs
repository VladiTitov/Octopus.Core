using RabbitMQ.Client;

namespace Octopus.Core.Loader.RabbitMq.Context
{
    public interface IRabbitMqContext
    {
        IConnection Connection { get; }
        IConnection GetRabbitConnection();
    }
}
