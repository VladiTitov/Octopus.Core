using System;
using System.Collections.Generic;

namespace Octopus.Core.Common.DynamicObject.Services.Interfaces
{
    public interface IDynamicObjectCreateService
    {
        IEnumerable<object> AddValuesToDynamicObject(Type extendedType, IEnumerable<string[]> values);

        Type CreateTypeByDescription();
    }
}
