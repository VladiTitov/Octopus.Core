using System.ComponentModel;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Models
{
    public class TableColumn
    {
        [Description("column_name")]
        public string PropertyName { get; set; }

        [Description("data_type")]
        public string PropertyTypeName { get; set; }
    }
}
