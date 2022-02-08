using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Octopus.Core.Common.Exceptions;
using Octopus.Core.Common.Extensions;
using Octopus.Core.Common.Models;
using Octopus.Core.Loader.WebApi.Core.Application.Interfaces;
using Octopus.Core.RabbitMq.Interfaces;

namespace Octopus.Core.Loader.WebApi.Core.Application.Services
{
    public class MessageHandler : IEventProcessor
    {
        private readonly ILogger<MessageHandler> _logger;
        private readonly IDynamicEntityService _dynamicEntityService;
        private readonly IDataReaderService _dataReaderService;

        public MessageHandler(ILogger<MessageHandler> logger,
            IDataReaderService dataReaderService,
            IDynamicEntityService dynamicEntityService)
        {
            _logger = logger;
            _dataReaderService = dataReaderService;
            _dynamicEntityService = dynamicEntityService;
        }

        public async void ProcessEvent(string message)
        {
            try
            {
                await LoadDataInDatabase(message.GetEntityDescription());
            }
            catch (ParsingException ex)
            {
                
            }
            catch (MongoDbException ex)
            {

            }
            catch (Exception ex)
            {

            }
        }

        public async Task LoadDataInDatabase(IEntityDescription entityDescription)
        {
            var objects = await _dataReaderService.GetDataFromFileAsync(entityDescription);
            await _dynamicEntityService.AddRangeAsync(objects);
        }
    }
}
