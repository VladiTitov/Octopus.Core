using System.Linq;
using System.Collections.Generic;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Services;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Extensions
{
    public static class TableColumnExtensions
    {
        public static bool IsEquals(this IEnumerable<ITableColumn> originalColumnsList, IEnumerable<ITableColumn> secondColumnsList)
            => originalColumnsList
                .SequenceEqual(
                    second: secondColumnsList,
                    comparer: new TableColumnComparer<ITableColumn>());
    }
}