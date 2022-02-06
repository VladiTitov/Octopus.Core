using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly IMongoContext _mongoContext;

        public DynamicEntitiesController(ILogger<DynamicEntitiesController> logger,
            IMongoRepository mongoRepository,
            IMongoContext mongoContext)
        {
            _logger = logger;
            _mongoContext = mongoContext;
            _mongoRepository = mongoRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DynamicProperty>> GetEntities()
        {
            throw new System.NotImplementedException();
        }

        [HttpPost]
        public ActionResult CreateEntity(DynamicEntityWithProperties entity)
        {
            _mongoRepository.Add(entity);
            return Ok("Entity created");
        }

        [HttpGet("{id}", Name = "GetEntityById")]
        public ActionResult<IEnumerable<DynamicProperty>> GetEntityById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
