﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Octopus.Core.Common.Helpers.JsonDeserializer
{
    public class JsonDeserializer : IJsonDeserializer
    {
        public IList<T> GetDynamicProperties<T>(string configDir) =>
            JsonSerializer.Deserialize<IList<T>>(File.ReadAllText(configDir));

        public async Task<IEnumerable<object>> GetDynamicObjects(Type extendedType, string fileName)
        {
            using (FileStream openStream = File.OpenRead(fileName))
            {
                var methodDeserialize = typeof(JsonSerializer).GetMethod("DeserializeAsync",
                    new[]
                {
                    typeof(Stream),
                    typeof(JsonSerializerOptions),
                    typeof(CancellationToken)
                });

                methodDeserialize = methodDeserialize.MakeGenericMethod(extendedType);

                return await (dynamic)methodDeserialize.Invoke(null,
                    new object[]
                    {
                        openStream,
                        null,
                        default
                    });
            }
        }
    }
}
