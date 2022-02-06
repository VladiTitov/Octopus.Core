using System.Collections.Generic;

namespace Octopus.Core.Common.DynamicObject.Models
{
    public class DynamicEntityWithProperties
    {
        public string EntityName { get; set; }
        public IEnumerable<DynamicProperty> Properties { get; set; }
    }
}
