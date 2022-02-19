using System.ComponentModel;
using Octopus.Core.Common.Extensions;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Models
{
    public class TableColumn
    {
        private string _propertyName;

        [Description("column_name")]
        public string PropertyName 
        {
            get => _propertyName;
            set => _propertyName = value.ToCamelCase();
        }

        private string _propertyTypeName;

        [Description("data_type")]
        public string PropertyTypeName
        {
            get => _propertyTypeName;
            set
            {
                if (value.Equals("text")) _propertyTypeName = "System.String";
                else if (value.Equals("integer")) _propertyTypeName = "System.Int32";
                else _propertyTypeName = value;
            }
        }
    }
}
