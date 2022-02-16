using Dapper;
using System.Data.Common;
using System.Threading.Tasks;
using System.Collections.Generic;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services
{
    public class QueryHandlerService : IQueryHandlerService
    {
        private readonly IDatabaseContext _context;
        private readonly IDatabaseExceptionFactory _exceptionFactory;

        public QueryHandlerService(IDatabaseContext context,
            IDatabaseExceptionFactory exceptionFactory)
        {
            _context = context;
            _exceptionFactory = exceptionFactory;
        }

        public async Task ExecuteAsync(string query)
        {
            try
            {
                using (var db = _context.CreateConnection())
                {
                    await db.ExecuteAsync(query);
                }
            }
            catch (DbException ex)
            {
                _exceptionFactory.GetDatabaseError(ex);
            }
        }

        public async Task ExecuteAsync(string query, IEnumerable<object> items)
        {
            try
            {
                using (var db = _context.CreateConnection())
                {
                    await db.ExecuteAsync(query, items);
                }
            }
            catch (DbException ex)
            {
                _exceptionFactory.GetDatabaseError(ex);
            }
        }
    }
}
