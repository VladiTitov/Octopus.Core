using System.Collections.Generic;
using System.Threading.Tasks;
using Octopus.Core.Common.DynamicObject.Services.Interfaces;
using Octopus.Core.Common.Helpers.JsonDeserializer;
using Octopus.Core.Loader.WebApi.Core.Application.Interfaces;

namespace Octopus.Core.Loader.WebApi.Core.Application.Services
{
    public class DataReaderService : IDataReaderService
    {
        private readonly IDynamicObjectCreateService _dynamicObjectCreate;
        private readonly IJsonDeserializer _jsonDeserializer;

        public DataReaderService(IDynamicObjectCreateService dynamicObjectCreate,
            IJsonDeserializer jsonDeserializer)
        {
            _dynamicObjectCreate = dynamicObjectCreate;
            _jsonDeserializer = jsonDeserializer;
        }

        public async Task<IEnumerable<object>> GetDataFromFileAsync(string filePath)
        {
            var typeListOf = typeof(List<>);
            var extendedType = _dynamicObjectCreate.CreateTypeByDescription(@"Configs\dynamicProperties.json");
            var typeListOfExtendedType = typeListOf.MakeGenericType(extendedType);

            return await _jsonDeserializer.GetDynamicObjects(typeListOfExtendedType, filePath);
        }
    }
}
