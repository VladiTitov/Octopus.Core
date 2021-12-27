using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Octopus.Core.Common.Configs;
using Octopus.Core.Common.DynamicObject.Services;
using Octopus.Core.Common.DynamicObject.Services.Interfaces;
using Octopus.Core.Common.Helpers.JsonDeserializer;
using Octopus.Core.Loader.BusinessLogic.Interfaces;
using Octopus.Core.Loader.BusinessLogic.Services;
using Octopus.Core.RabbitMq.Context;
using Octopus.Core.RabbitMq.Services.Implementations;
using Octopus.Core.RabbitMq.Services.Interfaces;
using Octopus.Core.RabbitMq.Workers;

namespace Octopus.Core.Loader.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(confBuilder =>
                {
                    confBuilder.AddJsonFile("appsettings.json");
                    confBuilder.AddJsonFile("Configs/rabbitMq-config.json");
                })
                .ConfigureServices((hostContext, services) =>
                { 
                    services.Configure<RabbitMqConfiguration>(hostContext.Configuration.GetSection("RabbitParams"));

                    services.AddSingleton<IRabbitMqContext, RabbitMqContext>();
                    services.AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>();
                    services.AddSingleton<IEventProcessor, MessageHandler>();

                    services.AddSingleton<IDynamicObjectCreateService, DynamicObjectCreateService>();
                    services.AddSingleton<IDynamicTypeFactory, DynamicTypeFactory>();

                    services.AddSingleton<IJsonDeserializer, JsonDeserializer>();
                    services.AddSingleton<IDataReaderService, DataReaderService>();

                    services.AddHostedService<MessageBusSubscriber>();
                });
    }
}
