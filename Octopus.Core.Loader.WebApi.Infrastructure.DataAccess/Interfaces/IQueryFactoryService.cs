﻿using System.Threading.Tasks;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces
{
    public interface IQueryFactoryService
    {
        string GetInsertQuery(object item);
        Task<string> GetCreateTableQuery(string entityName);
        string GetCreateSchemeQuery();
        Task<string> GetCreateCommentQuery(string entityName);
    }
}
