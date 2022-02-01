using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octopus.Core.Loader.WebApi.Core.Application.Interfaces
{
    public interface IDataReaderService
    {
        Task<IEnumerable<object>> GetDataFromFileAsync(string filePath);
    }
}
