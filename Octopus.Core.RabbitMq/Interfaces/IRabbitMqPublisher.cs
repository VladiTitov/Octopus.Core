using System.Threading.Tasks;

namespace Octopus.Core.RabbitMq.Interfaces
{
    public interface IRabbitMqPublisher
    {
        Task SendMessage(string message);
    }
}
