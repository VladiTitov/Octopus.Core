using System;
using System.Collections.Generic;
using Octopus.Core.Common.DynamicObject.Models;

namespace Octopus.Core.Common.DynamicObject.Services.Interfaces
{
    public interface IDynamicTypeFactory
    {
        Type CreateNewTypeWithDynamicProperty(Type parentType, IEnumerable<DynamicProperty> dynamicProperties);
    }
}
