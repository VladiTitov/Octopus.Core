﻿using Microsoft.Extensions.Options;
using Octopus.Core.Common.Constants;
using Octopus.Core.Common.Exceptions;
using Octopus.Core.Parser.WorkerService.Configuration.Implementations;
using Octopus.Core.Parser.WorkerService.Interfaces.Services.DynamicModels;
using Octopus.Core.Parser.WorkerService.Services.Parsers.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Octopus.Core.Parser.WorkerService.Services.Parsers
{
    public class JSONParser : BaseParser
    {
        private readonly JsonParserConfiguration _options;

        public JSONParser(IOptions<JsonParserConfiguration> options, IDynamicObjectCreateService dynamicObjectCreateService)
            : base(dynamicObjectCreateService)
        {
            _options = options.Value;
        }

        public async override Task<IEnumerable<object>> Parse(FileInfo inputFile, string modelDescriptionPath)
        {
            Type typeListOfExtendedType;

            try
            {
                var typeListOf = typeof(List<>);
                var extendedType = _dynamicObjectCreateService.CreateTypeByDescription(modelDescriptionPath);
                typeListOfExtendedType = typeListOf.MakeGenericType(extendedType);
            }
            catch (Exception ex)
            {
                throw new DynamicServiceException($"{ErrorMessages.DynamicServiceException} {ex.Message}");
            }

            try
            {
                return await GetObjects(typeListOfExtendedType, inputFile.FullName);
            }
            catch (Exception ex)
            {
                throw new ParsingException($"{ErrorMessages.JsonParserException} {ex.Message}");
            }            
        }

        private async Task<IEnumerable<object>> GetObjects(Type extendedType, string fileName)
        {
            using (FileStream openStream = File.OpenRead(fileName))
            {
                var methodDeserialize = typeof(JsonSerializer).GetMethod("DeserializeAsync", new[] { typeof(Stream), typeof(JsonSerializerOptions), typeof(CancellationToken) });

                methodDeserialize = methodDeserialize.MakeGenericMethod(extendedType);

                return await (dynamic)methodDeserialize.Invoke(null, new object[] { openStream, null, default });
            }
        }
    }
}
