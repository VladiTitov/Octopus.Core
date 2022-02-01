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
        public ActionResult StartLoaderService()
        {
            _logger.LogInformation($"Starting service at: {DateTime.Now}");
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("Stop")]
        public ActionResult StopLoaderService()
        {
            _logger.LogInformation($"Stopping service at: {DateTime.Now}");
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("Balancer")]
        public ActionResult BalancerService()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("Statistic")]
        public ActionResult StatisticService()
        {
            throw new NotImplementedException();
        }
    }
}
