using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Octopus.Core.Common.Constants;
using Octopus.Core.Common.Exceptions;
using Octopus.Core.Parser.WorkerService.Interfaces.Services;
using System;
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
            try
            {
                await _parserProcessor.StartProcessing(stoppingToken);
            }
            catch (IncorrectInputDataException ex)
            {

            }
            catch (QueueException ex)
            {

            }
            catch (ParsingException ex)
            {

            }
            catch (DynamicServiceException ex)
            {

            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ErrorMessages.UnhandledException);
            }
            finally
            {
                await _parserProcessor.StopProcessing(stoppingToken);
            }
        }
    }
}
