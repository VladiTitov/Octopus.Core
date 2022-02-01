using Octopus.Core.Loader.BusinessLogic.Interfaces;
using Octopus.Core.Loader.DataAccess.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octopus.Core.Loader.BusinessLogic.Services
{
    public class DynamicEntityService : IDynamicEntityService
    {
        private readonly IDynamicEntityRepository _repository;

        public DynamicEntityService(IDynamicEntityRepository repository)
        {
            _repository = repository;
        }

        public async Task AddRangeAsync(IEnumerable<object> items) => await _repository.AddRange(items);

    }
}
