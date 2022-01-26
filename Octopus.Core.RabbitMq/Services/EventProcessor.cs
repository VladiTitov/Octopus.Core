using Octopus.Core.RabbitMq.Services.Interfaces;
using System;

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
