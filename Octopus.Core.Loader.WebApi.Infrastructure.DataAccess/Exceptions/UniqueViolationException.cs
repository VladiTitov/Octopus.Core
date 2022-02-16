using System;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Exceptions
{
    public class UniqueViolationException : Exception
    {
        public UniqueViolationException(string msg) : base(msg)
        {

        }
    }
}
