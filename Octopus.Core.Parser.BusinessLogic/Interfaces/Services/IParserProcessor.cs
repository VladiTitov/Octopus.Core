using Octopus.Core.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Octopus.Core.Parser.BusinessLogic.Interfaces.Services
{
    public interface IParserProcessor
    {
        Task Parse(IEntityDescription request);

        Task StartProcessing();

        Task StopProcessing();
    }
}
