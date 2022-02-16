using System.Data.Common;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Constants;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Exceptions;
using Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Services
{
    public class DatabaseExceptionFactory : IDatabaseExceptionFactory
    {
        public void GetDatabaseError(DbException exception)
        {
            var msg = exception.Message;
            throw exception.SqlState switch
            {
                DatabaseExceptionCodeConstants.InvalidSchemaName => new InvalidSchemaNameException(msg),
                DatabaseExceptionCodeConstants.UndefinedTable => new UndefinedTableException(msg),
                DatabaseExceptionCodeConstants.UniqueViolation => new UniqueViolationException(msg),
                DatabaseExceptionCodeConstants.NotNullViolation => new NotNullViolationException(msg),
                DatabaseExceptionCodeConstants.UndefinedColumn => new UndefinedColumnException(msg),
                _ => exception
            };
        }
    }
}
