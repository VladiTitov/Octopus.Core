using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Services
{
    public class TableColumnComparer<T> : IEqualityComparer<T> where T : ITableColumn
    {
        public bool Equals(T x, T y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (ReferenceEquals(x, null)
                || ReferenceEquals(y, null))
                return false;

            return x.PropertyName.Equals(y.PropertyName) 
                   && x.PropertyTypeName.Equals(y.PropertyTypeName)
                   && x.PropertyIsNullable.Equals(y.PropertyIsNullable);
        }

        public int GetHashCode([DisallowNull] T obj)
        {
            throw new NotImplementedException();
        }
    }
}