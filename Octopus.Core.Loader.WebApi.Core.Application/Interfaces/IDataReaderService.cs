using System.Threading.Tasks;
using System.Collections.Generic;
using Octopus.Core.Common.Models;

namespace Octopus.Core.Loader.WebApi.Core.Application.Interfaces
{
    public interface IDataReaderService
    {
        Task<IEnumerable<object>> GetDataFromFileAsync(IEntityDescription entityDescription);
    }
}
