using Octopus.Core.Common.Models;

namespace Octopus.Core.RabbitMq.Services.Interfaces
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}
