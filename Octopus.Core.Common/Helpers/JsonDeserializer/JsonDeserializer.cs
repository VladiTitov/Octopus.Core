using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Octopus.Core.Common.Helpers.JsonDeserializer
{
    public class JsonDeserializer : IJsonDeserializer
    {
        public IList<T> GetDynamicProperties<T>(string configDir) =>
            JsonSerializer.Deserialize<IList<T>>(File.ReadAllText(configDir));
    }
}
