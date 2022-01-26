using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Octopus.Core.RabbitMq.Services.Interfaces
{
    public interface IRabbitMqSubscriber
    {
        Task Execute(IModel channel, string queueName);
    }
}
