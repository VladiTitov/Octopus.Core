namespace Octopus.Core.RabbitMq.Interfaces
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}
