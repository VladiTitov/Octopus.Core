using System;

namespace Octopus.Core.Loader.WebApi.Infrastructure.DataAccess.Exceptions
{
    public class InvalidSchemaNameException : Exception
    {
        public InvalidSchemaNameException(string msg) : base(msg)
        {

        }
    }
}
