using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octopus.Core.Loader.DataAccess.Repositories
{
    public interface IDynamicEntityRepository
    {
        Task AddRange(IEnumerable<object> items);
    }
}
