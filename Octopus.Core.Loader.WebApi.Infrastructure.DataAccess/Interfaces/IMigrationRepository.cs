using System.Collections.Generic;
using System.Threading.Tasks;
using Octopus.Core.Common.DynamicObject.Models;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces
{
    public interface IMigrationRepository
    {
        Task CreateSchemeAsync();
        Task CreateTableAsync(DynamicEntityWithProperties dynamicEntity);
        void TableCheck(DynamicEntityWithProperties dynamicEntity);
        Task<IEnumerable<DynamicEntityProperty>> GetTableColumnsAsync(string tableName);
    }
}
