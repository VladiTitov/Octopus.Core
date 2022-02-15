using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octopus.Core.Loader.DataAccess.Interfaces
{
    public interface IQueryHandlerService
    {
        Task Execute(string query);
        Task Execute(string query, IEnumerable<object> items);
    }
}
