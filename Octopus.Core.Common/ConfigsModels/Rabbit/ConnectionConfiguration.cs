using Octopus.Core.Common.ConfigsModels.Rabbit.Interfaces;

namespace Octopus.Core.Common.ConfigsModels.Rabbit
{
    public class ConnectionConfiguration : IConnectionConfiguration
    {
        public string Hostname { get; set; }
        public int Port { get; set; }
        public string VirtualHost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
