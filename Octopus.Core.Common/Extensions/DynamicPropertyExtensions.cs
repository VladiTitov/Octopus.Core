using Octopus.Core.Common.DynamicObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Octopus.Core.Common.Extensions
{
    public static class DynamicPropertyExtensions
    {
        public static IEnumerable<string> GetPropertiesNames(this IList<DynamicProperty> properties)
        {
            if (properties == null) throw new ArgumentNullException(nameof(properties));

            return properties.Select(property => property.GetTableStroke());
        }

        public static string GetTableStroke(this DynamicProperty property) =>
            $"{property.GetTablePropertyName()} {property.GetTablePropertyType()} {property.PrimaryKeyCheck()} {property.NotNullCheck()}";

        public static string GetTablePropertyName(this DynamicProperty property) => property.PropertyName.GetProperty();

        public static string GetTablePropertyType(this DynamicProperty property) => property.DynamicEntityDataBaseProperty.DataBaseTypeName;

        public static string PrimaryKeyCheck(this DynamicProperty property) => property.DynamicEntityDataBaseProperty.IsKey.SetPrimaryKey();

        public static string NotNullCheck(this DynamicProperty property) => property.DynamicEntityDataBaseProperty.IsNotNull.SetNotNull();
    }
}
