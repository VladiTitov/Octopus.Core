using System;

namespace Octopus.Core.Common.Exceptions
{
    public class QueueException : Exception
    {
        public QueueException(string msg): base(msg)
        {

        }
    }
}
