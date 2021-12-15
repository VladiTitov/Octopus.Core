using Octopus.Core.Common.Models;
using Octopus.Core.Parser.WorkerService.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace Octopus.Core.Parser.WorkerService.Services
{
    public class QueueConsumer : IQueueConsumer
    {
        public async Task<IEntityDescription> ConsumeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
