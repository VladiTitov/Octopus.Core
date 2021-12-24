using FluentScheduler;
using Octopus.Core.Loader.BusinessLogic.Jobs;
using Octopus.Core.RabbitMq.Services.Interfaces;

namespace Octopus.Core.Loader.BusinessLogic.Services
{
    public class JobRegistryService : Registry
    {
        public JobRegistryService()
        {
            Schedule(() => new QueueConsumer())
                .NonReentrant()
                .ToRunNow()
                .AndEvery(1)
                .Seconds();
        }
    }
}
