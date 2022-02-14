using System;

namespace Octopus.Core.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string msg) : base(msg)
        {
        }
    }
}
