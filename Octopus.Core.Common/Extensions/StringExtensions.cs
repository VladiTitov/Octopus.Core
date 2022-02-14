using System;
using System.Text.Json;
using System.Collections.Generic;
using Octopus.Core.Common.Models;
using Octopus.Core.Common.Exceptions;

namespace Octopus.Core.Common.Extensions
{
    public static class StringExtensions
    {
        public static IEntityDescription GetEntityDescription(this string item)
        {
            try
            {
                return JsonSerializer.Deserialize<EntityDescription>(item);
            }
            catch (Exception ex)
            {
                throw new ParsingException(ex.Message);
            }
        }

        public static string GetProperty(this string item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            return $"\"{item}\"";
        }

        public static string GetValue(this string item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            return $"@{item}";
        }

        public static string ToQuery(this IEnumerable<string> items, string separator)
        {
            return string.Join(separator, items);
        }

        public static int GetValidIntProperty(this string item)
        {
            if (item == null) return 0;
            int.TryParse(item, out var value);
            return value;
        }

        public static DateTime GetValidDateTimeProperty(this string item)
        {
            if (item == null) return DateTime.MinValue;
            DateTime.TryParse(item, out var value);
            return value;
        }

        public static string ToCamelCase(this string item)
        {
            var firstPartUpper = char.ToUpper(item[0]);
            var secondPartLower = item.Substring(1).ToLower();
            return $"{firstPartUpper}{secondPartLower}";
        }
    }
}
