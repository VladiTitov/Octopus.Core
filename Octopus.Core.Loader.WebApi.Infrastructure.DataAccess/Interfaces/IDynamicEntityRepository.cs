using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces
{
    public interface IDynamicEntityRepository
    {
        Task AddRange(string query, IEnumerable<object> items);
    }
}
