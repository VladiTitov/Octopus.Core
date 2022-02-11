using System.Threading.Tasks;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services
{
    public class MigrationCreateService : IMigrationCreateService
    {
        
        private readonly IQueryFactoryService _queryFactory;
        private readonly IQueryHandlerService _queryHandler;
        private DynamicEntityWithProperties _dynamicEntity;

        public MigrationCreateService(IQueryFactoryService queryFactory,
            IQueryHandlerService queryHandler)
        {
            _queryFactory = queryFactory;
            _queryHandler = queryHandler;
        }

        public async Task CreateMigrationAsync(DynamicEntityWithProperties dynamicEntity)
        {
            _dynamicEntity = dynamicEntity;
            await CreateSchemeAsync();
            await CreateTableAsync();
        }

        public async Task CreateTableAsync()
        {
            var query = _queryFactory.GetCreateTableQuery(_dynamicEntity);
            await _queryHandler.Execute(query);
        }

        public async Task CreateSchemeAsync()
        {
            var query = _queryFactory.GetCreateSchemeQuery();
            await _queryHandler.Execute(query);
        }
            
    }
}
