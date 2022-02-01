using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octopus.Core.Loader.WebApi.Core.Application.Interfaces
{
    public interface IDynamicEntityService
    {
        Task AddRangeAsync(IEnumerable<object> items);
    }
}
