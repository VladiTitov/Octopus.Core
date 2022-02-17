using System.ComponentModel;

namespace Octopus.Core.Common.DynamicObject.Models
{
    public class DynamicEntityProperty
    {
        [Description("column_name")]
        public string PropertyName { get; set; }

        [Description("data_type")]
        public string PropertyTypeName { get; set; }
    }
}
