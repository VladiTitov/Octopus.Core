using System.Data;

namespace Octopus.Core.Loader.DataAccess.DatabaseContext
{
    public interface IDatabaseContext
    {
        IDbConnection CreateConnection();
    }
}
