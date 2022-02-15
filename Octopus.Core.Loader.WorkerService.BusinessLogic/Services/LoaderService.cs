using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Octopus.Core.Common.ConfigsModels.Rabbit;
using Octopus.Core.RabbitMq.Services.Interfaces;

namespace Octopus.Core.Loader.BusinessLogic.Services
{
    public class LoaderService : BackgroundService
    {
        private readonly IRabbitMqPublisher _rabbitMqPublisher;
        private readonly IRabbitMqSubscriber _rabbitMqSubscriber;
        private readonly IEventProcessor _eventProcessor;
        private readonly IOptions<RabbitMqConfiguration> _configuration;

        private readonly ILogger<LoaderService> _logger;

        public LoaderService(ILogger<LoaderService> logger,
            IRabbitMqPublisher rabbitMqPublisher,
            IRabbitMqSubscriber rabbitMqSubscriber,
            IOptions<RabbitMqConfiguration> configuration,
            IEventProcessor eventProcessor)
        {
            _rabbitMqPublisher = rabbitMqPublisher;
            _rabbitMqSubscriber = rabbitMqSubscriber;
            _configuration = configuration;
            _eventProcessor = eventProcessor;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _rabbitMqPublisher.SendMessage("Hello World!");

            return Task.CompletedTask;
        }
    }
}
