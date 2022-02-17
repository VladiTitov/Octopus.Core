﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Octopus.Core.Common.Constants;
using Octopus.Core.Common.Exceptions;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Loader.WebApi.Core.Application.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Exceptions;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;

namespace Octopus.Core.Loader.WebApi.Core.Application.Services
{
    public class DynamicEntityService : IDynamicEntityService
    {
        private readonly ILogger<DynamicEntityService> _logger;
        private readonly IDynamicEntityRepository _repository;
        private readonly IQueryFactoryService _queryFactory;
        private readonly IMigrationService _migrationService;
        private readonly IMongoRepository _mongoRepository;

        private DynamicEntityWithProperties _dynamicEntity;
        private string _query;

        public DynamicEntityService(ILogger<DynamicEntityService> logger,
            IDynamicEntityRepository repository,
            IQueryFactoryService queryFactory,
            IMigrationService migrationService,
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
            if (_dynamicEntity == null) _dynamicEntity = await GetDynamicEntityAsync(items);
            if (_query == null) _query = _queryFactory.GetInsertQuery(_dynamicEntity);

            try
            {
                await _repository.AddRangeAsync(_query, items);
            }
            catch (InvalidSchemaNameException ex)
            {
                _logger.LogError(ex.Message);
                _migrationService.InvalidSchemaNameHandler();
                await AddRangeAsync(items);
            }
            catch (UndefinedTableException ex)
            {
                _logger.LogError(ex.Message);
                _migrationService.UndefinedTableHandler(_dynamicEntity);
                await AddRangeAsync(items);
            }
            catch (UniqueViolationException ex)
            {
                _logger.LogError(ex.Message);
                _migrationService.UniqueViolationNameHandler(_dynamicEntity);
            }
            catch (NotNullViolationException ex)
            {
                _logger.LogError(ex.Message);
                _migrationService.NotNullViolationHandler();
            }
            catch (UndefinedColumnException ex)
            {
                _logger.LogError(ex.Message);
                _migrationService.UndefinedColumnHandler();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private async Task<DynamicEntityWithProperties> GetDynamicEntityAsync(IEnumerable<object> items)
        {
            var entityName = items.FirstOrDefault().GetType().Name;
            var entity = await _mongoRepository.GetEntity(entityName);

            if (entity == null) throw new NotFoundException($"{ErrorMessages.DynamicEntityNotFound}{entityName}");

            return entity;
        }
    }
}
