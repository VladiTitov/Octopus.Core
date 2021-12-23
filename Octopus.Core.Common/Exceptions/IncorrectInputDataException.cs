using System;

namespace Octopus.Core.Common.Exceptions
{
    public class IncorrectInputDataException : Exception
    {
        public IncorrectInputDataException(string msg) : base(msg)
        {

        }
    }
}
