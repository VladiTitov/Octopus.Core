using System.Threading.Tasks;
using System.Collections.Generic;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Models;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Interfaces
{
    public interface IPostgresMigrationService
    {
        Task CreateSchemeAsync();
        Task CreateTableAsync(DynamicEntityWithProperties dynamicEntity);
        void TableCheck(DynamicEntityWithProperties dynamicEntity);
        Task<IEnumerable<TableColumn>> GetTableColumnsAsync(string tableName);
    }
}
