using Octopus.Core.Common.Models;
using Octopus.Core.Loader.BusinessLogic.Interfaces;
using Octopus.Core.RabbitMq.Services.Interfaces;
using System.Text.Json;

namespace Octopus.Core.Loader.BusinessLogic.Services
{
    public class MessageHandler : IEventProcessor
    {
        private IEntityDescription _entityDescription;
        private readonly IDynamicEntityService _dynamicEntityService;
        private readonly IDataReaderService _dataReaderService;

        public MessageHandler(IDataReaderService dataReaderService,
            IDynamicEntityService dynamicEntityService)
        {
            _dataReaderService = dataReaderService;
            _dynamicEntityService = dynamicEntityService;
        }

        public async void ProcessEvent(string message)
        {
            _entityDescription = JsonSerializer.Deserialize<EntityDescription>(message);
            var objects = await _dataReaderService.GetDataFromFileAsync(_entityDescription.EntityFilePath);
            await _dynamicEntityService.AddRangeAsync(objects);
        }
    }
}
