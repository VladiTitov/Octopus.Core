using Octopus.Core.Common.Models;
using System.Threading.Tasks;

namespace Octopus.Core.Parser.BusinessLogic.Interfaces.Services
{
    public interface IValidationService
    {
        Task<ParserInputData> ValidateEntityDescription(IEntityDescription request);
    }
}
