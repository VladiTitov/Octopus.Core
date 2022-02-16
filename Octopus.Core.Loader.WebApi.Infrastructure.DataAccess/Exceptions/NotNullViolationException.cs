using System;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Exceptions
{
    public class NotNullViolationException : Exception
    {
        public NotNullViolationException(string msg) : base(msg)
        {

        }
    }
}
