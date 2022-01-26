using System;
using System.Linq;
using System.Reflection;

namespace Octopus.Core.Common.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static string GetPropertiesNames(this PropertyInfo[] properties) => GetNamesString(properties);

        public static string GetValuesNames(this PropertyInfo[] properties) => GetNamesString(properties, false);

        private static string GetNamesString(PropertyInfo[] properties, bool isProperty = true)
        {
            if (properties == null) throw new ArgumentNullException(nameof(properties));

            var items = properties.Select(i => GetItemString(i.Name, isProperty));

            return string.Join(',', items);
        }

        private static string GetItemString(string itemString, bool isProperty = true)
        {
            return isProperty ? itemString.GetProperty() : itemString.GetValue();
        }
    }
}
