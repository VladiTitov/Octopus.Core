using System.ComponentModel;
using System.Runtime.InteropServices;
using Octopus.Core.Common.Extensions;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Services;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Models
{
    public class PostgresTableColumn : ITableColumn
    {
        private readonly IPostgresTypeConverterService _postgresTypeConverter;

        public PostgresTableColumn()
        {
            _postgresTypeConverter = new PostgresTypeConverterService();
        }

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
            set => _propertyTypeName = _postgresTypeConverter.GetSystemType(value);
        }

        [Description("is_nullable")]
        private string PropertyIsNullableString
        {
            set => PropertyIsNullable = ConvertToBoolean(value);
        }

        public bool PropertyIsNullable { get; set; }

        private bool ConvertToBoolean(string value)
        {
            return value switch
            {
                "NO" => false,
                "YES" => true,
                _ => throw new InvalidOleVariantTypeException(),
            };
        }
    }
}
