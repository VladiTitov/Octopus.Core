using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Octopus.Core.Parser.WorkerService.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Octopus.Core.Parser.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IParserProcessor _parserProcessor;

        public Worker(ILogger<Worker> logger, IParserProcessor parserProcessor)
        {
            _logger = logger;
            _parserProcessor = parserProcessor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _parserProcessor.StartProcessing(stoppingToken);
        }
    }
}
