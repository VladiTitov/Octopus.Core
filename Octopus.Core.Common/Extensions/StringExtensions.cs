﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Octopus.Core.Common.Extensions
{
    public static class StringExtensions
    {
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

        public static string ToCamelCase(this string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            return string.Join(".", name.Split('.').Select(n => char.ToLower(n[0]) + n.Substring(1)));
        }
    }
}