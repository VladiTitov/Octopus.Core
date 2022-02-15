using Octopus.Core.Common.Exceptions;
using Octopus.Core.Common.Extensions;
using Octopus.Core.Common.Models;
using Octopus.Core.Parser.BusinessLogic.Interfaces.Services;
using Octopus.Core.RabbitMq.Interfaces;
using System;

namespace Octopus.Core.Parser.BusinessLogic.Services
{
    public class MessageHandler : IEventProcessor
    {
        private readonly IParserProcessor _parserProcessor;
        private readonly IValidationService _validationService;

        public MessageHandler(IParserProcessor parserProcessor, IValidationService validationService)
        {
            _parserProcessor = parserProcessor;
            _validationService = validationService;
        }

        public async void ProcessEvent(string message)
        {
            try
            {
                var parserInputData = await _validationService.ValidateEntityDescription(message.GetEntityDescription());

                await _parserProcessor.ProcessInputData(parserInputData);
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
