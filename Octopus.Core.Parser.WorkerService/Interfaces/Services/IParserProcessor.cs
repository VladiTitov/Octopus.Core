using System.Threading;
using System.Threading.Tasks;

namespace Octopus.Core.Parser.WorkerService.Interfaces.Services
{
    public interface IParserProcessor
    {
        Task StartProcessing(CancellationToken stoppingToken);

        Task StopProcessing(CancellationToken stoppingToken);
    }
}
