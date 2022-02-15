using Octopus.Core.Common.Models;
using System.Threading.Tasks;

namespace Octopus.Core.Parser.BusinessLogic.Interfaces.Services
{
    public interface IParserProcessor
    {
        Task StartProcessing();

        Task StopProcessing();

        Task ProcessInputData(ParserInputData inputData);
    }
}
