using System;
using System.Text.Json.Serialization;

namespace Octopus.Core.Common.DynamicObject.Models
{
    public class DynamicProperty
    {
        public string PropertyName { get; set; }
        public string SystemTypeName { get; set; }
        public int ValueIndex { get; set; }

        [JsonIgnore]
        public Type SystemType => Type.GetType(SystemTypeName);

        public DynamicEntityDataBaseProperty DynamicEntityDataBaseProperty { get; set; }
    }
}
