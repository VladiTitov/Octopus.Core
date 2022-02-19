using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Models;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Services
{
    public class TableColumnComparer : IEqualityComparer<TableColumn>
    {
        public bool Equals(TableColumn x, TableColumn y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (ReferenceEquals(x, null)
                || ReferenceEquals(y, null))
                return false;

            return x.PropertyName.Equals(y.PropertyName) 
                   && x.PropertyTypeName.Equals(y.PropertyTypeName);
        }

        public int GetHashCode([DisallowNull] TableColumn obj)
        {
            throw new NotImplementedException();
        }
    }
}