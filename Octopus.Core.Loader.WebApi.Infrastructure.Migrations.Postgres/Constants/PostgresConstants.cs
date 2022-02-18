namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Constants
{
    public static class PostgresConstants
    {
        public const string DatabaseColumnsList = "information_schema.columns";
        public const string DatabaseSchemaList = "pg_catalog.pg_namespace";
        public const string AllSchemesColumn = "nspname";
        public const string TableName = "table_name";
        public const string ColumnName = "column_name";
        public const string DataType = "data_type";
    }
}
