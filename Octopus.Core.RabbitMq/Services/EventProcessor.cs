using System;
using Octopus.Core.RabbitMq.Interfaces;

namespace Octopus.Core.RabbitMq.Services
{
    public class EventProcessor : IEventProcessor
    {
        public void ProcessEvent(string message)
        {
            Console.WriteLine(message);
        }
    }
}
