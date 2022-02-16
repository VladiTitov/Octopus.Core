using System;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Exceptions
{
    public class UndefinedTableException : Exception
    {
        public UndefinedTableException(string msg) : base(msg)
        {

        }
    }
}
