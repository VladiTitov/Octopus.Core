using System.Threading.Tasks;
using System.Collections.Generic;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Repositories
{
    public class DynamicEntityRepository : IDynamicEntityRepository
    {
        private readonly IQueryHandlerService _queryHandler;

        public DynamicEntityRepository(IQueryHandlerService queryHandler)
        {
            _queryHandler = queryHandler;
        }

        public async Task AddRange(string query, IEnumerable<object> items)
        {
            await _queryHandler.Execute(query, items);
        }
    }
}
