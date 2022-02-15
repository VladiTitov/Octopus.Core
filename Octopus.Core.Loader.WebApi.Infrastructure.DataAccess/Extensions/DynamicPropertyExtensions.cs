using System.Linq;
using System.Collections.Generic;
using Octopus.Core.Common.Extensions;
using Octopus.Core.Common.DynamicObject.Models;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Extensions
{
    public static class DynamicPropertyExtensions
    {
        public static IEnumerable<string> GetPropertiesNames(this IList<DynamicProperty> properties) 
            => properties
                .Select(property => property.GetTableRow());

        public static string GetTableRow(this DynamicProperty property)
            => $"{property.GetTablePropertyName()} " +
               $"{property.GetTablePropertyType()} " +
               $"{property.PrimaryKeyCheck()} " +
               $"{property.NotNullCheck()}";

        public static string GetTablePropertyName(this DynamicProperty property) 
            => property
                .PropertyName
                .GetProperty();

        public static string GetTablePropertyType(this DynamicProperty property)
            => property
                .DynamicEntityDataBaseProperty
                .DataBaseTypeName;

        public static string PrimaryKeyCheck(this DynamicProperty property) 
            => property
                .DynamicEntityDataBaseProperty
                .IsKey
                .SetPrimaryKey();

        public static string NotNullCheck(this DynamicProperty property) 
            => property
                .DynamicEntityDataBaseProperty
                .IsNotNull
                .SetNotNull();
    }
}