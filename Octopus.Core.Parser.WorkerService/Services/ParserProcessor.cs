using Octopus.Core.Parser.WorkerService.Interfaces.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Octopus.Core.Parser.WorkerService.Services
{
    public class ParserProcessor : IParserProcessor
    {
        private readonly IQueueConsumer _consumer;

        public ParserProcessor(IQueueConsumer consumer)
        {
            _consumer = consumer;
        }

        public async Task StartProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var request = await _consumer.ConsumeAsync();

                if (request != null)
                {

                }

                await Task.Delay(1000, stoppingToken);
            }
        }

        public async Task StopProcessing(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
