using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Octopus.Core.Common.ConfigsModels.Rabbit;
using Octopus.Core.RabbitMq.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Octopus.Core.Parser.BusinessLogic.Services
{
    public class ParserService : BackgroundService
    {
        private readonly IRabbitMqPublisher _rabbitMqPublisher;
        private readonly IRabbitMqSubscriber _rabbitMqSubscriber;
        private readonly IEventProcessor _eventProcessor;
        private readonly IOptions<RabbitMqConfiguration> _configuration;

        private readonly ILogger<ParserService> _logger;

        public ParserService(ILogger<ParserService> logger, 
            IRabbitMqPublisher rabbitMqPublisher,
            IRabbitMqSubscriber rabbitMqSubscriber,
            IOptions<RabbitMqConfiguration> configuration,
            IEventProcessor eventProcessor)
        {
            _logger = logger;
            _rabbitMqPublisher = rabbitMqPublisher;
            _rabbitMqSubscriber = rabbitMqSubscriber;
            _configuration = configuration;
            _eventProcessor = eventProcessor;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
