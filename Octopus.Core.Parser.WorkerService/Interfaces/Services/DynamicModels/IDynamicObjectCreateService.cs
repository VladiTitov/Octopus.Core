using System.Collections.Generic;

namespace Octopus.Core.Parser.WorkerService.Interfaces.Services.DynamicModels
{
    public interface IDynamicObjectCreateService
    {
        IEnumerable<object> AddValuesToDynamicObject(string dynamicPropertiesFilePath, IEnumerable<string[]> values);
    }
}
