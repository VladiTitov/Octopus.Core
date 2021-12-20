using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octopus.Core.Common.Helpers.JsonDeserializer
{
    public interface IJsonDeserializer
    {
        IList<T> GetDynamicProperties<T>(string configDir);
    }
}
