using Octopus.Core.Common.DynamicObject.Services.Interfaces;
using Octopus.Core.Common.Helpers.JsonDeserializer;
using Octopus.Core.Loader.BusinessLogic.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octopus.Core.Loader.BusinessLogic.Services
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
            var extendedType = _dynamicObjectCreate.CreateTypeByDescription();
            var typeListOfExtendedType = typeListOf.MakeGenericType(extendedType);

            return await _jsonDeserializer.GetDynamicObjects(typeListOfExtendedType, filePath);
        }
    }
}
