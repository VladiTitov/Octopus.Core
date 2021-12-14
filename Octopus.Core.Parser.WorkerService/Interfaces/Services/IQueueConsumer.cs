using Octopus.Core.Common.Models;
using System.Threading.Tasks;

namespace Octopus.Core.Parser.WorkerService.Interfaces.Services
{
    public interface IQueueConsumer
    {
        Task<IEntityDescription> ConsumeAsync();
    }
}
