using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octopus.Core.Loader.WebApi.Application.Interfaces
{
    public interface IDataReaderService
    {
        Task<IEnumerable<object>> GetDataFromFileAsync(string filePath);
    }
}
