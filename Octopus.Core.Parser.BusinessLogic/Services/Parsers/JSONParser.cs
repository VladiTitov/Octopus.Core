using Microsoft.Extensions.Options;
using Octopus.Core.Common.ConfigsModels.Parsers;
using Octopus.Core.Common.Constants;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Common.DynamicObject.Services.Interfaces;
using Octopus.Core.Common.Exceptions;
using Octopus.Core.Parser.BusinessLogic.Services.Parsers.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Octopus.Core.Parser.BusinessLogic.Services.Parsers
{
    public class JSONParser : BaseParser
    {
        private readonly JsonParserConfiguration _options;

        public JSONParser(IOptions<JsonParserConfiguration> options, 
            IDynamicObjectCreateService dynamicObjectCreateService)
            : base(dynamicObjectCreateService)
        {
            _options = options.Value;
        }

        public async override Task<IEnumerable<object>> Parse(FileInfo inputFile, 
            DynamicEntityWithProperties modelDescription)
        {
            Type extendedType;

            try
            {
                extendedType = BuildGenericExtendedType(modelDescription);
            }
            catch (Exception ex)
            {
                throw new DynamicServiceException($"{ErrorMessages.DynamicServiceException} {ex.Message}");
            }

            try
            {
                return await GetObjects(extendedType, inputFile.FullName);
            }
            catch (Exception ex)
            {
                throw new ParsingException($"{ErrorMessages.JsonParserException} {ex.Message}");
            }
        }

        private Type BuildGenericExtendedType(DynamicEntityWithProperties modelDescription)
        {
            var typeListOf = typeof(List<>);

            var extendedType = _dynamicObjectCreateService.CreateTypeByDescription(modelDescription);

            return typeListOf.MakeGenericType(extendedType);
        }

        private async Task<IEnumerable<object>> GetObjects(Type extendedType, string fileName)
        {
            using (FileStream openStream = File.OpenRead(fileName))
            {
                var methodDeserialize = typeof(JsonSerializer).GetMethod("DeserializeAsync", 
                    new[] 
                    { 
                        typeof(Stream), typeof(JsonSerializerOptions), typeof(CancellationToken) 
                    });

                methodDeserialize = methodDeserialize.MakeGenericMethod(extendedType);

                return await (dynamic)methodDeserialize.Invoke(null, new object[] { openStream, null, default });
            }
        }
    }
}
