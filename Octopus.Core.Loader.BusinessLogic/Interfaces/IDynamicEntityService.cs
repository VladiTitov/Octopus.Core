using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octopus.Core.Loader.BusinessLogic.Interfaces
{
    public interface IDynamicEntityService
    {
        Task AddRangeAsync(IEnumerable<object> items);
    }
}
