using System.Threading.Tasks;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Services
{
    public class PostgresMigrationService : IPostgresMigrationService
    {
        private readonly ISchemaHandler _schemaHandler;
        private readonly ITableHandler _tableHandler;

        public PostgresMigrationService(ISchemaHandler schemaHandler,
            ITableHandler tableHandler)
        {
            _schemaHandler = schemaHandler;
            _tableHandler = tableHandler;
        }

        public async Task CreateSchemeAsync() 
            => await _schemaHandler.CreateSchemeAsync();
        
        public async Task CreateTableAsync(DynamicEntityWithProperties dynamicEntity)
        {
            if (!await _schemaHandler.IsExistSchema()) await _schemaHandler.CreateSchemeAsync();
            await _tableHandler.CreateTableAsync(dynamicEntity);
        }

        public async Task TableCheckAsync(DynamicEntityWithProperties dynamicEntity) 
            => await _tableHandler.TableCheck(dynamicEntity);
    }
}

