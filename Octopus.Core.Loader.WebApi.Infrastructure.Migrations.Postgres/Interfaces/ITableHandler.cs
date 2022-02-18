using System.Threading.Tasks;
using System.Collections.Generic;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Models;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Postgres.Interfaces
{
    public interface ITableHandler
    {
        Task CreateTableAsync(DynamicEntityWithProperties dynamicEntity);
        Task TableCheck(DynamicEntityWithProperties dynamicEntity);
        Task<IEnumerable<TableColumn>> GetTableColumnsAsync(string tableName);
    }
}
