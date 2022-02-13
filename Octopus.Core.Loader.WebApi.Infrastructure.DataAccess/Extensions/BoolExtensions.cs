using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Constants;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Extensions
{
    public static class BoolExtensions
    {
        public static string SetPrimaryKey(this bool value)
            => value ? QueryConstants.PrimaryKeyQuery : string.Empty;

        public static string SetNotNull(this bool value)
            => value ? QueryConstants.NotNullQuery : string.Empty;
    }
}
