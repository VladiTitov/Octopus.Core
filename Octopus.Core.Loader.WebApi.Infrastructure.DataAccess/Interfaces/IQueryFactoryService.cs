using System.Collections.Generic;
using Octopus.Core.Common.DynamicObject.Models;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces
{
    public interface IQueryFactoryService
    {
        string GetCreateSchemeQuery();
        string GetInsertQuery(object item, string entityName);
        string GetCreateTableQuery(string entityName, IList<DynamicProperty> dynamicProperties);
        string GetCreateCommentQuery(string entityName, IList<DynamicProperty> dynamicProperties);
    }
}
