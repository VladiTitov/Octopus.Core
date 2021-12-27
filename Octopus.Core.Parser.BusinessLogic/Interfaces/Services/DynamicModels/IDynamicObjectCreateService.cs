using System;
using System.Collections.Generic;

namespace Octopus.Core.Parser.WorkerService.Interfaces.Services.DynamicModels
{
    public interface IDynamicObjectCreateService_2
    {
        IEnumerable<object> AddValuesToDynamicObject(Type extendedType, IEnumerable<string[]> values);

        Type CreateTypeByDescription(string dynamicPropertiesFilePath);
    }
}
