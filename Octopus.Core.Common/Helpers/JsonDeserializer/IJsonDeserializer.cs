using System.Collections.Generic;

namespace Octopus.Core.Common.Helpers.JsonDeserializer
{
    public interface IJsonDeserializer
    {
        IList<T> GetDynamicProperties<T>(string configDir);
    }
}
