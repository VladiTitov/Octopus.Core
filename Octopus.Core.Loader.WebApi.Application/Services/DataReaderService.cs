using System.Collections.Generic;
using System.Threading.Tasks;
using Octopus.Core.Loader.WebApi.Application.Interfaces;

namespace Octopus.Core.Loader.WebApi.Application.Services
{
    public class DataReaderService : IDataReaderService
    {
        public Task<IEnumerable<object>> GetDataFromFileAsync(string filePath)
        {
            throw new System.NotImplementedException();
        }
    }
}
