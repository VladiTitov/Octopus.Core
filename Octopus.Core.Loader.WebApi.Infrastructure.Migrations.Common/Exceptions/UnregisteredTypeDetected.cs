using System;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Exceptions
{
    public class UnregisteredTypeDetected : Exception
    {
        public UnregisteredTypeDetected(string msg) : base(msg)
        {

        }
    }
}
