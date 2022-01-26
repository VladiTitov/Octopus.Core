namespace Octopus.Core.Common.ConfigsModels.Rabbit
{
    public class RabbitMqConfiguration
    {
        public string ExchangeType { get; set; }
        public string ExchangeName { get; set; }
        public string QueueName { get; set; }
        public string RoutingKey { get; set; }
    }
}
