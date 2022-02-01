using Dapper;
using Octopus.Core.Loader.DataAccess.DatabaseContext;
using Octopus.Core.Loader.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octopus.Core.Loader.DataAccess.Services
{
    public class QueryHandlerService : IQueryHandlerService
    {
        private readonly IDatabaseContext _context;

        public QueryHandlerService(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Execute(string query)
        {
            using (var db = _context.CreateConnection())
            {
                await db.ExecuteAsync(query);
            }
        }

        public async Task Execute(string query, IEnumerable<object> items)
        {
            using (var db = _context.CreateConnection())
            {
                await db.ExecuteAsync(query, items);
            }
        }
    }
}
