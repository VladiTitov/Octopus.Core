using System;

namespace Octopus.Core.Common.Exceptions
{
    public class DatabaseProviderException : Exception
    {
        public DatabaseProviderException(string msg) : base(msg)
        {
            
        }
    }
}
