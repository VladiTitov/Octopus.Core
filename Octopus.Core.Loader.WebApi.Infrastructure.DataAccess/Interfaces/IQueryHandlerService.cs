using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces
{
    public interface IQueryHandlerService
    {
        Task ExecuteAsync(string query);
        Task ExecuteAsync(string query, IEnumerable<object> items);
    }
}
