using Octopus.Core.Common.Models;
using System.Threading.Tasks;

namespace Octopus.Core.Parser.BusinessLogic.Interfaces.Services
{
    public interface IParserProcessor
    {
        Task ProcessInputData(ParserInputData inputData);

        Task StartProcessing();

        Task StopProcessing();
    }
}
