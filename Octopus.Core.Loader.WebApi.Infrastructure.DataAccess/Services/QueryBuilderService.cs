﻿using System;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services
{
    public class QueryBuilderService : IQueryBuilderService, IDisposable
    {
        private string _query;

        public QueryBuilderService AddPart(string queryPart)
        {
            _query += queryPart;
            return this;
        }

        public QueryBuilderService AddSeparator(string separator)
        {
            _query += separator;
            return this;
        }

        public string GetQuery() => _query;

        public void Dispose() => _query = null;
    }
}
