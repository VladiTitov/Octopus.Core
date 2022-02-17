using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octopus.Core.Common.Models;
using Octopus.Core.Common.Constants;
using Octopus.Core.Common.Exceptions;
using Octopus.Core.Common.Helpers.JsonDeserializer;
using Octopus.Core.Common.DynamicObject.Services.Interfaces;
using Octopus.Core.Loader.WebApi.Core.Application.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Interfaces;

namespace Octopus.Core.Loader.WebApi.Core.Application.Services
{
    public class DataReaderService : IDataReaderService
    {
        private readonly IDynamicObjectCreateService _dynamicObjectCreate;
        private readonly IJsonDeserializer _jsonDeserializer;
        private readonly IMongoRepository _mongoRepository;

        public DataReaderService(IMongoRepository mongoRepository,
            IDynamicObjectCreateService dynamicObjectCreate,
            IJsonDeserializer jsonDeserializer)
        {
            _mongoRepository = mongoRepository;
            _dynamicObjectCreate = dynamicObjectCreate;
            _jsonDeserializer = jsonDeserializer;
        }

        public async Task<IEnumerable<object>> GetDataFromFileAsync(IEntityDescription entityDescription)
            => await _jsonDeserializer
                .GetDynamicObjects(
                    type: await GetDynamicType(entityDescription.EntityType), 
                    fileName: entityDescription.EntityFilePath);

        private async Task<Type> GetDynamicType(string dynamicEntityName)
        {
            var dynamicEntity = await _mongoRepository.GetEntity(dynamicEntityName);
            if (dynamicEntity == null) throw new NotFoundException($"{ErrorMessages.DynamicEntityNotFound}{dynamicEntityName}");
            
            var typeListOf = typeof(List<>);
            var extendedType = _dynamicObjectCreate.CreateTypeByDescription(dynamicEntity);
            return typeListOf.MakeGenericType(extendedType);
        }
    }
}
