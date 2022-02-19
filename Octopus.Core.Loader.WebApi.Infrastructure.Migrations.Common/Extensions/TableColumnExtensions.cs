using System.Linq;
using System.Collections.Generic;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Models;
using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Services;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Extensions
{
    public static class TableColumnExtensions
    {
        public static bool IsEquals(this IEnumerable<TableColumn> originalColumnsList, IEnumerable<TableColumn> secondColumnsList)
        {
            return originalColumnsList.SequenceEqual(secondColumnsList, new TableColumnComparer());
        }
    }
}