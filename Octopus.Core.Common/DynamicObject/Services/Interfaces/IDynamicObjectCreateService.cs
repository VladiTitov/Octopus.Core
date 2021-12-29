using Octopus.Core.Common.DynamicObject.Models;
using System;
using System.Collections.Generic;

namespace Octopus.Core.Common.DynamicObject.Services.Interfaces
{
    public interface IDynamicObjectCreateService
    {
        IEnumerable<object> AddValuesToDynamicObject(Type extendedType, IEnumerable<string[]> values);
        IList<DynamicProperty> ConfigureDynamicProperties(string pathFile);
        Type CreateTypeByDescription(string dynamicPropertiesFilePath);
    }
}
