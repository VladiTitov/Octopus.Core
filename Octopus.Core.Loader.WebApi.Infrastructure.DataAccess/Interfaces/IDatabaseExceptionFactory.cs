using System.Data.Common;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces
{
    public interface IDatabaseExceptionFactory
    {
        void GetDatabaseError(DbException exception);
    }
}