using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octopus.Core.Loader.BusinessLogic.Interfaces
{
    public interface IDataReaderService
    {
        Task<IEnumerable<object>> GetDataFromFileAsync(string filePath);
    }
}
