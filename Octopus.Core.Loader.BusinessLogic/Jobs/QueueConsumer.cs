using FluentScheduler;
using Octopus.Core.RabbitMq.Services.Interfaces;

namespace Octopus.Core.Loader.BusinessLogic.Jobs
{
    public class QueueConsumer : IJob
    {
        private readonly IRabbitMqListener _listener;

        public QueueConsumer(IRabbitMqListener listener)
        {
            _listener = listener;
        }

        public void Execute()
        {
            _listener.ChannelConsume();
        }
    }
}
