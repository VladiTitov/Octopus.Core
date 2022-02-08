using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Octopus.Core.Loader.WebApi.Core.Application.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;

namespace Octopus.Core.Loader.WebApi.Core.Application.Services
{
    public class DynamicEntityService : IDynamicEntityService
    {
        private readonly ILogger<DynamicEntityService> _logger;
        private readonly IDynamicEntityRepository _repository;
        private readonly IQueryFactoryService _queryFactory;
        private readonly IMigrationCreateService _migrationService;

        public DynamicEntityService(ILogger<DynamicEntityService> logger,
            IDynamicEntityRepository repository,
            IQueryFactoryService queryFactory,
            IMigrationCreateService migrationService)
        {
            _logger = logger;
            _repository = repository;
            _queryFactory = queryFactory;
            _migrationService = migrationService;
        }

        public async Task AddRangeAsync(IEnumerable<object> items)
        {
            var firstItemInCollection = items.FirstOrDefault();
            var query = GetQuery(firstItemInCollection);

            try
            {
                await _repository.AddRange(query, items);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                await _migrationService.CreateMigrationAsync(firstItemInCollection.GetType().Name);
                await AddRangeAsync(items);
            }
        }
            
        public string GetQuery(object item)
        {
            var entityName = item.GetType().Name;
            return _queryFactory.GetInsertQuery(item, entityName);
        }
    }
}
