using System.Collections.Generic;
using System.Threading.Tasks;
using Octopus.Core.Loader.WebApi.Core.Application.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;

namespace Octopus.Core.Loader.WebApi.Core.Application.Services
{
    public class DynamicEntityService : IDynamicEntityService
    {
        private readonly IDynamicEntityRepository _repository;

        public DynamicEntityService(IDynamicEntityRepository repository)
        {
            _repository = repository;
        }

        public async Task AddRangeAsync(IEnumerable<object> items) 
            => await _repository.AddRange(items);
    }
}
