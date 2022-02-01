using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Octopus.Core.Common.ConfigsModels.Rabbit.Base;
using Octopus.Core.RabbitMq.Services.Interfaces;

namespace Octopus.Core.Loader.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly ILogger<ManagerController> _logger;
        private readonly IRabbitMqSubscriber _rabbitMqSubscriber;
        private readonly IEnumerable<SubscriberConfiguration> _subscribersConfiguration;

        public ManagerController(ILogger<ManagerController> logger,
            IRabbitMqSubscriber rabbitMqSubscriber,
            IEnumerable<SubscriberConfiguration> subscribersConfiguration)
        {
            _logger = logger;
            _rabbitMqSubscriber = rabbitMqSubscriber;
            _subscribersConfiguration = subscribersConfiguration;
        }

        [HttpGet]
        [Route("Start")]
        public ActionResult StartLoaderService()
        {
            _logger.LogInformation($"Starting service at: {DateTime.Now}");
            _rabbitMqSubscriber.StartService(_subscribersConfiguration);
            return Ok("Service started");
        }

        [HttpGet]
        [Route("Stop")]
        public ActionResult StopLoaderService()
        {
            _logger.LogInformation($"Stopping service at: {DateTime.Now}");
            _rabbitMqSubscriber.StopService();
            return Ok("Service stopped");
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
