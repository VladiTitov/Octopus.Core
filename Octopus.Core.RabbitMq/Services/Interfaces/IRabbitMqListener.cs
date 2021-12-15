using RabbitMQ.Client.Events;

namespace Octopus.Core.RabbitMq.Services.Interfaces
{
    public interface IRabbitMqListener
    {
        void ChannelConsume();
        EventingBasicConsumer GetConsumer();
    }
}
