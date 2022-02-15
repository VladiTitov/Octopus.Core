using System;

namespace Octopus.Core.Common.Exceptions
{
    public class MongoDbException : Exception
    {
        public MongoDbException(string msg) : base(msg)
        {

        }
    }
}
