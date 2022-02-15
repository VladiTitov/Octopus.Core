using System.Linq;
using System.Collections.Generic;

namespace Octopus.Core.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static string GetPropertiesNames(this IEnumerable<string> properties)
            => string
                .Join(separator: ",",
                    values: properties
                        .Select(i => i.GetProperty()));

        public static string GetValuesNames(this IEnumerable<string> properties)
            => string
                .Join(separator: ",",
                    values: properties
                        .Select(i => i.GetValue()));
    }
}
