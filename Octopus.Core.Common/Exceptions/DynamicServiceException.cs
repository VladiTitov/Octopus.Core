using System;

namespace Octopus.Core.Common.Exceptions
{
    public class DynamicServiceException : Exception
    {
        public DynamicServiceException(string msg): base(msg)
        {

        }
    }
}
