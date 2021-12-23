using Octopus.Core.Common.Models;
using System;
using System.Collections.Generic;

namespace Octopus.Core.Parser.WorkerService.Interfaces.Services.DynamicModels
{
    public interface IDynamicTypeFactory
    {
        Type CreateNewTypeWithDynamicProperty(Type parentType, IEnumerable<DynamicProperty> dynamicProperties);
    }
}
