namespace Octopus.Core.RabbitMq.Services
{
    public interface IRabbitMqInitializer
    {
        public void InitializeRabbitMq();
        public void Dispose();
    }
}
