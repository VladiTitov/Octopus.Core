using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Octopus.Core.Common.Exceptions;
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

        public MessageHandler(ILogger<MessageHandler> logger, IDataReaderService dataReaderService,
            IDynamicEntityService dynamicEntityService)
        {
            _logger = logger;
            _dataReaderService = dataReaderService;
            _dynamicEntityService = dynamicEntityService;
        }

        public async void ProcessEvent(string message)
        {
            var entityDescription = GetEntityDescription(message);
            if (entityDescription != null) await LoadDataInDatabase(entityDescription);
        }

        public async Task LoadDataInDatabase(IEntityDescription entityDescription)
        {
            var objects = await _dataReaderService.GetDataFromFileAsync(entityDescription);
            await _dynamicEntityService.AddRangeAsync(objects);
        }

        public IEntityDescription GetEntityDescription(string message)
        {
            try
            {
                return JsonSerializer.Deserialize<EntityDescription>(message);
            }
            catch (ParsingException ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
