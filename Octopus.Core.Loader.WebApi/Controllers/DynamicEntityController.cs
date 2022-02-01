using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Octopus.Core.Loader.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicEntityController : ControllerBase
    {
        private readonly ILogger<DynamicEntityController> _logger;

        public DynamicEntityController(ILogger<DynamicEntityController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<object>> GetEntities()
        {
            throw new System.NotImplementedException();
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<object>> GetEntityById(int id)
        {
            throw new System.NotImplementedException();
        }

        [HttpPost]
        public ActionResult<IEnumerable<object>> CreateEntity(object entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
