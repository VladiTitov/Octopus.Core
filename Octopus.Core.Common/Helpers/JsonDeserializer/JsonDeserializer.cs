using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Octopus.Core.Common.Helpers.JsonDeserializer
{
    public class JsonDeserializer : IJsonDeserializer
    {
        public async Task<IEnumerable<object>> GetDynamicObjects(Type extendedType, string fileName)
        {
            using (FileStream openStream = File.OpenRead(fileName))
            {
                var typeOfParameters = new[]
                {
                    typeof(Stream),
                    typeof(JsonSerializerOptions),
                    typeof(CancellationToken)
                };

                var methodDeserialize = typeof(JsonSerializer).GetMethod("DeserializeAsync", typeOfParameters);

                methodDeserialize = methodDeserialize.MakeGenericMethod(extendedType);

                return await (dynamic)methodDeserialize.Invoke(null, new object[] { openStream, null, default });
            }
        }
    }
}
