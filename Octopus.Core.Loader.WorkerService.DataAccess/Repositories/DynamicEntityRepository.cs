﻿using Octopus.Core.Loader.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Octopus.Core.Loader.DataAccess.Repositories
{
    public class DynamicEntityRepository : IDynamicEntityRepository
    {
        private readonly IQueryFactoryService _queryFactory;
        private readonly IQueryHandlerService _queryHandler;
        private readonly IMigrationCreateService _migrationService;

        public DynamicEntityRepository(IQueryFactoryService queryFactory,
            IQueryHandlerService queryHandler,
            IMigrationCreateService migrationService)
        {
            _queryFactory = queryFactory;
            _queryHandler = queryHandler;
            _migrationService = migrationService;
        }

        public async Task AddRange(IEnumerable<object> items)
        {
            var query = _queryFactory.GetInsertQuery(items.FirstOrDefault());
            try
            {
                await _queryHandler.Execute(query, items);
            }
            catch
            {
                await _migrationService.CreateMigrationAsync();
                await this.AddRange(items);
            }
        }
    }
}
