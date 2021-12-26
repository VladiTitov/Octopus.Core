using Octopus.Core.Common.DynamicObject.Services.Interfaces;
using Octopus.Core.Common.Helpers.JsonDeserializer;
using Octopus.Core.Common.Models;
using Octopus.Core.RabbitMq.Services.Interfaces;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Octopus.Core.Loader.BusinessLogic.Services
{
    public class MessageHandler : IEventProcessor
    {
        private IEntityDescription _entityDescription;
        private readonly IDynamicObjectCreateService _dynamicObjectCreate;
        private readonly IJsonDeserializer _jsonDeserializer;

        public MessageHandler(IDynamicObjectCreateService dynamicObjectCreate, 
            IJsonDeserializer jsonDeserializer) 
        {
            _dynamicObjectCreate = dynamicObjectCreate;
            _jsonDeserializer = jsonDeserializer;
        }

        public async void ProcessEvent(string message)
        {
            _entityDescription = JsonSerializer.Deserialize<EntityDescription>(message);

            var objects = await GetDataFromFileAsync(_entityDescription.EntityFilePath);
        }

        private async Task<IEnumerable<object>> GetDataFromFileAsync(string filePath)
        {
            var typeListOf = typeof(List<>);
            var extendedType = _dynamicObjectCreate.CreateTypeByDescription();
            var typeListOfExtendedType = typeListOf.MakeGenericType(extendedType);

            return await _jsonDeserializer.GetDynamicObjects(typeListOfExtendedType, _entityDescription.EntityFilePath);
        }
    }
}
