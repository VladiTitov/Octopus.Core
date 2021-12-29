namespace Octopus.Core.Common.Configs
{
    public abstract class RabbitMqConfiguration
    {
        public string Hostname { get; set; }
        public int Port { get; set; }
        public string VirtualHost { get; set; }
        public string ExchangeType { get; set; }
        public string ExchangeName { get; set; }
        public string QueueName { get; set; }
        public string RoutingKey { get; set;}
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
