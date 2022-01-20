using System.Threading.Tasks;

namespace Octopus.Core.RabbitMq.Services.Interfaces
{
    public interface IRabbitMqPublisher
    {
        Task SendMessage(string message);
    }
}
