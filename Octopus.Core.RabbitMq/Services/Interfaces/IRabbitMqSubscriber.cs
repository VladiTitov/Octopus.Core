using System.Threading.Tasks;

namespace Octopus.Core.RabbitMq.Services.Interfaces
{
    public interface IRabbitMqSubscriber
    {
        Task Execute();
    }
}
