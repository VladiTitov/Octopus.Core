using Octopus.Core.Common.Constants;

namespace Octopus.Core.Common.Extensions
{
    public static class BoolExtension
    {
        public static string SetPrimaryKey(this bool value) => value ? QueryConstants.PrimaryKeyQuery : string.Empty;

        public static string SetNotNull(this bool value) => value ? QueryConstants.NotNullQuery : string.Empty;
    }
}
