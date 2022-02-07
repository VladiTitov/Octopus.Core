using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Repositories
{
    public class DynamicEntityRepository : IDynamicEntityRepository
    {
        private readonly ILogger<DynamicEntityRepository> _logger;
        private readonly IQueryFactoryService _queryFactory;
        private readonly IQueryHandlerService _queryHandler;
        private readonly IMigrationCreateService _migrationService;

        public DynamicEntityRepository(ILogger<DynamicEntityRepository> logger,
            IQueryFactoryService queryFactory,
            IQueryHandlerService queryHandler,
            IMigrationCreateService migrationService)
        {
            _logger = logger;
            _queryFactory = queryFactory;
            _queryHandler = queryHandler;
            _migrationService = migrationService;
        }

        public async Task AddRange(IEnumerable<object> items, string entityName)
        {
            var query = _queryFactory.GetInsertQuery(items.FirstOrDefault(), entityName);
            try
            {
                await _queryHandler.Execute(query, items);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                await _migrationService.CreateMigrationAsync(entityName);
                await AddRange(items, entityName);
            }
        }
    }
}
