namespace Octopus.Core.RabbitMq.Services.Interfaces
{
    public interface IRabbitMqPublisher
    {
        void ChannelConsume(string message);
    }
}
