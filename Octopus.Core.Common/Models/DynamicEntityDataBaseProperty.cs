namespace Octopus.Core.Common.Models
{
    public class DynamicEntityDataBaseProperty
    {
        public string DataBaseTypeName { get; set; }

        public int Length { get; set; }

        public bool IsNotNull { get; set; }

        public bool IsKey { get; set; }

        public string Comment { get; set; }
    }
}
