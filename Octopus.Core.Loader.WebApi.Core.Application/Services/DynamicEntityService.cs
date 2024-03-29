﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Octopus.Core.Common.Constants;
using Octopus.Core.Common.Exceptions;
using Octopus.Core.Loader.WebApi.Core.Application.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Interfaces;

namespace Octopus.Core.Loader.WebApi.Core.Application.Services
{
    public class DynamicEntityService : IDynamicEntityService
    {
        private readonly ILogger<DynamicEntityService> _logger;
        private readonly IDynamicEntityRepository _repository;
        private readonly IQueryFactoryService _queryFactory;
        private readonly IMigrationCreateService _migrationService;
        private readonly IMongoRepository _mongoRepository;

        public DynamicEntityService(ILogger<DynamicEntityService> logger,
            IDynamicEntityRepository repository,
            IQueryFactoryService queryFactory,
            IMigrationCreateService migrationService,
            IMongoRepository mongoRepository)
        {
            _logger = logger;
            _repository = repository;
            _queryFactory = queryFactory;
            _migrationService = migrationService;
            _mongoRepository = mongoRepository;
        }

        public async Task AddRangeAsync(IEnumerable<object> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));

            var entityName = items.FirstOrDefault().GetType().Name;
            var dynamicEntity = await _mongoRepository.GetEntity(entityName);

            if (dynamicEntity == null) throw new NotFoundException($"{ErrorMessages.DynamicEntityNotFound}{entityName}");

            var query = _queryFactory.GetInsertQuery(dynamicEntity);

            try
            {
                await _repository.AddRange(query, items);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                await _migrationService.CreateMigrationAsync(dynamicEntity);
                await AddRangeAsync(items);
            }
        }
    }
}
