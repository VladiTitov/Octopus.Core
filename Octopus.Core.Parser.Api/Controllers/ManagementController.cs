using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Octopus.Core.Common.ConfigsModels.Rabbit.Base;
using Octopus.Core.Common.Constants;
using Octopus.Core.RabbitMq.Interfaces;
using System;
using System.Collections.Generic;

namespace Octopus.Core.Parser.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementController : ControllerBase
    {
        private readonly ILogger<ManagementController> _logger;
        private readonly IRabbitMqSubscriber _rabbitMqSubscriber;
        private readonly IEnumerable<SubscriberConfiguration> _subscribersConfiguration;

        public ManagementController(ILogger<ManagementController> logger, 
            IRabbitMqSubscriber rabbitMqSubscriber, 
            IEnumerable<SubscriberConfiguration> subscribersConfiguration)
        {
            _logger = logger;
            _rabbitMqSubscriber = rabbitMqSubscriber;
            _subscribersConfiguration = subscribersConfiguration;
        }

        /// <summary>
        /// Метод для запуска экземпляра парсера
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Start")]
        public IActionResult StartParserService()
        {
            _logger.LogInformation($"Starting service at: {DateTime.Now}"); 
            _rabbitMqSubscriber.StartService(_subscribersConfiguration);

            return Ok(ApiStatusMessages.ServiceStartedMessage);
        }

        /// <summary>
        /// Метод для остановки экземпляра парсера
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Stop")]
        public IActionResult StopParserService()
        {
            _logger.LogInformation($"Stopping service at: {DateTime.Now}");
            _rabbitMqSubscriber.StopService();

            return Ok(ApiStatusMessages.ServiceStoppedMessage);
        }

        /// <summary>
        /// Балансировка нагрузки работы парсера
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [Route("Balancer")]
        public IActionResult BalancerService()
        {
            throw new NotImplementedException();
        }
    }
}
