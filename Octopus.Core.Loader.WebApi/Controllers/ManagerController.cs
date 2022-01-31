using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Octopus.Core.Loader.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly ILogger<ManagerController> _logger;

        public ManagerController(ILogger<ManagerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Start")]
        public void StartLoaderService()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("Stop")]
        public void StopLoaderService()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("Balancer")]
        public void BalancerService()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("Statistic")]
        public void StatisticService()
        {
            throw new NotImplementedException();
        }
    }
}
