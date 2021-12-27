using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octopus.Core.Common.Helpers.JsonDeserializer
{
    public interface IJsonDeserializer
    {
        IList<T> GetDynamicProperties<T>(string configDir);

        Task<IEnumerable<object>> GetDynamicObjects(Type type, string fileName);
    }
}
