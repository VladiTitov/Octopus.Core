using Octopus.Core.Common.Exceptions;
using Octopus.Core.Common.Models;
using Octopus.Core.Parser.WorkerService.Interfaces.Services;
using Octopus.Core.RabbitMq.Services.Interfaces;
using System;
using System.Text.Json;

namespace Octopus.Core.Parser.BusinessLogic.Services
{
    public class MessageHandler : IEventProcessor
    {
        private IEntityDescription _entityDescription;
        private readonly IParserProcessor _parserProcessor;

        public MessageHandler(IParserProcessor parserProcessor)
        {
            _parserProcessor = parserProcessor;

            _parserProcessor.StartProcessing();
        }

        public async void ProcessEvent(string message)
        {
            try
            {
                _entityDescription = JsonSerializer.Deserialize<EntityDescription>(message);
                await _parserProcessor.Parse(_entityDescription);
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
                
            }
            finally
            {
                await _parserProcessor.StopProcessing();
            }
        }
    }
}
