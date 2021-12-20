using System.Collections.Generic;

namespace Octopus.Core.Parser.WorkerService.Interfaces.Services.DynamicModels
{
    public interface IDynamicObjectCreateService
    {
        IEnumerable<object> AddValuesToDynamicObject(IEnumerable<string[]> values);
    }
}
