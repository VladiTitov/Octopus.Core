using System.Threading.Tasks;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services
{
    public class MigrationCreateService : IMigrationCreateService
    {
        private readonly IQueryFactoryService _queryFactory;
        private readonly IQueryHandlerService _queryHandler;

        public MigrationCreateService(IQueryFactoryService queryFactory,
            IQueryHandlerService queryHandler)
        {
            _queryFactory = queryFactory;
            _queryHandler = queryHandler;
        }

        public async Task CreateMigrationAsync(string entityName)
        {
            await CreateSchemeAsync(entityName);
            await CreateTableAsync(entityName);
        }

        public async Task CreateTableAsync(string entityName) 
        {
            var query = await _queryFactory.GetCreateTableQuery(entityName);
            await _queryHandler.Execute(query);
        }
            

        public async Task CreateSchemeAsync(string entityName) =>
            await _queryHandler.Execute(_queryFactory.GetCreateSchemeQuery());
    }
}
