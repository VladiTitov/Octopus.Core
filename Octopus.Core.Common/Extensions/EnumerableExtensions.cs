using System;
using System.Collections.Generic;
using System.Linq;

namespace Octopus.Core.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static string GetPropertiesNames(this IEnumerable<string> properties) => GetNamesString(properties);
        public static string GetValuesNames(this IEnumerable<string> properties) => GetNamesString(properties, false);
        private static string GetNamesString(IEnumerable<string> properties, bool isProperty = true)
        {
            if (properties == null) throw new ArgumentNullException(nameof(properties));

            var items = properties.Select(i => GetItemString(i, isProperty));

            return string.Join(',', items);
        }

        private static string GetItemString(string itemString, bool isProperty = true)
            => isProperty ? itemString.GetProperty() : itemString.GetValue();
    }
}
