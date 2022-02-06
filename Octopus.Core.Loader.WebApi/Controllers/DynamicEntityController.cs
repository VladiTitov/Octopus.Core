using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Octopus.Core.Common.Constants;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Interfaces;

namespace Octopus.Core.Loader.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicEntitiesController : ControllerBase
    {
        private readonly ILogger<DynamicEntitiesController> _logger;
        private readonly IMongoRepository _mongoRepository;

        public DynamicEntitiesController(ILogger<DynamicEntitiesController> logger,
            IMongoRepository mongoRepository,
            IMongoContext mongoContext)
        {
            _logger = logger;
            _mongoRepository = mongoRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DynamicProperty>> GetEntities()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult CreateEntity(DynamicEntityWithProperties entity)
        {
            _logger.LogInformation($"Entity added at: {DateTime.Now}");
            _mongoRepository.AddEntity(entity);
            return Ok(ApiStatusMessages.DynamicEntityAddedMessage);
        }

        [HttpGet("{id}", Name = "GetEntityById")]
        public ActionResult<IEnumerable<DynamicProperty>> GetEntityById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
