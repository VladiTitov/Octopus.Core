using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Octopus.Core.Common.DynamicObject.Services.Interfaces;
using Octopus.Core.Common.Helpers.JsonDeserializer;
using Octopus.Core.Common.Models;
using Octopus.Core.Loader.WebApi.Core.Application.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Interfaces;

namespace Octopus.Core.Loader.WebApi.Core.Application.Services
{
    public class DataReaderService : IDataReaderService
    {
        private readonly IDynamicObjectCreateService _dynamicObjectCreate;
        private readonly IJsonDeserializer _jsonDeserializer;
        private readonly ILogger<DataReaderService> _logger;
        private readonly IMongoRepository _mongoRepository;

        public DataReaderService(ILogger<DataReaderService> logger,
            IMongoRepository mongoRepository,
            IDynamicObjectCreateService dynamicObjectCreate,
            IJsonDeserializer jsonDeserializer)
        {
            _logger = logger;
            _mongoRepository = mongoRepository;
            _dynamicObjectCreate = dynamicObjectCreate;
            _jsonDeserializer = jsonDeserializer;
        }

        public async Task<IEnumerable<object>> GetDataFromFileAsync(IEntityDescription entityDescription)
        {
            var typeListOf = typeof(List<>);

            var dynamicEntity = await _mongoRepository.GetEntity(entityDescription.EntityType);
            var extendedType = _dynamicObjectCreate.CreateTypeByDescription(dynamicEntity);
            var typeListOfExtendedType = typeListOf.MakeGenericType(extendedType);

            return await _jsonDeserializer.GetDynamicObjects(typeListOfExtendedType, entityDescription.EntityFilePath);
        }
    }
}
