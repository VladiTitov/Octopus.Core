namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Constants
{
    public static class DatabaseExceptionCodeConstants
    {
        public const string InvalidSchemaName = "3F000";
        public const string UndefinedTable = "42P01";
        public const string UniqueViolation = "23505";
        public const string NotNullViolation = "23502";
        public const string UndefinedColumn = "42703";
    }
}
