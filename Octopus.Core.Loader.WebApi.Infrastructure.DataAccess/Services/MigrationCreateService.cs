using System.Threading.Tasks;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services
{
    public class MigrationCreateService : IMigrationCreateService
    {
        private readonly IMongoRepository _mongoRepository;
        private readonly IQueryFactoryService _queryFactory;
        private readonly IQueryHandlerService _queryHandler;

        public MigrationCreateService(IQueryFactoryService queryFactory,
            IQueryHandlerService queryHandler,
            IMongoRepository mongoRepository)
        {
            _queryFactory = queryFactory;
            _queryHandler = queryHandler;
            _mongoRepository = mongoRepository;
        }

        public async Task CreateMigrationAsync(string entityName)
        {
            await CreateSchemeAsync(entityName);
            await CreateTableAsync(entityName);
        }

        public async Task CreateTableAsync(string entityName)
        {
            var dynamicEntity = await _mongoRepository.GetEntity(entityName);
            var query = _queryFactory.GetCreateTableQuery(entityName, dynamicEntity.Properties);
            await _queryHandler.Execute(query);
        }

        public async Task CreateSchemeAsync(string entityName) =>
            await _queryHandler.Execute(_queryFactory.GetCreateSchemeQuery());
    }
}
