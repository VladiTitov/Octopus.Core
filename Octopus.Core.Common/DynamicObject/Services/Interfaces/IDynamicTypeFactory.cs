using System;
using System.Collections.Generic;
using Octopus.Core.Common.DynamicObject.Models;

namespace Octopus.Core.Common.DynamicObject.Services.Interfaces
{
    public interface IDynamicTypeFactory
    {
        Type CreateNewTypeWithDynamicProperty(Type parentType, string typeName, IEnumerable<DynamicProperty> dynamicProperties);
        Type GetTypeWithDynamicProperty(Type parentType, IEnumerable<DynamicProperty> dynamicProperties);
    }
}
