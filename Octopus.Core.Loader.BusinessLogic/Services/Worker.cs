using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using FluentScheduler;

namespace Octopus.Core.Loader.BusinessLogic.Services
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger, JobRegistryService jobRegistryService)
        {
            _logger = logger;
            JobManager.Initialize(jobRegistryService);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            JobManager.Start();
        }
    }
}
