using Octopus.Core.Loader.DataAccess.Interfaces;
using System.Threading.Tasks;

namespace Octopus.Core.Loader.DataAccess.Services
{
    public class MigrationCreateService : IMigrationCreateService
    {
        private readonly IQueryFactoryService _queryFactory;
        private readonly IQueryHandlerService _queryHandler;

        public MigrationCreateService(IQueryFactoryService queryFactory, IQueryHandlerService queryHandler)
        {
            _queryFactory = queryFactory;
            _queryHandler = queryHandler;
        }

        public async Task CreateMigrationAsync()
        {
            await CreateSchemeAsync();
            await CreateTableAsync();
        }

        public async Task CreateTableAsync() =>
            await _queryHandler.Execute(_queryFactory.GetCreateTableQuery());

        public async Task CreateSchemeAsync() =>
            await _queryHandler.Execute(_queryFactory.GetCreateSchemeQuery());
    }
}
