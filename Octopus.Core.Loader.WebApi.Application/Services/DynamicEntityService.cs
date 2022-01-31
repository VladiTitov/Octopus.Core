using System.Collections.Generic;
using System.Threading.Tasks;
using Octopus.Core.Loader.WebApi.Application.Interfaces;

namespace Octopus.Core.Loader.WebApi.Application.Services
{
    public class DynamicEntityService : IDynamicEntityService
    {
        public async Task AddRangeAsync(IEnumerable<object> items)
        {
            throw new System.NotImplementedException();
        }

    }
}
