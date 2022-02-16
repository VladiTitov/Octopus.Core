using System;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Exceptions
{
    public class UndefinedColumnException : Exception
    {
        public UndefinedColumnException(string msg) : base(msg)
        {

        }
    }
}
