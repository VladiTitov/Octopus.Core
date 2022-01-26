using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Octopus.Core.Common.ConfigsModels.Rabbit;
using Octopus.Core.Common.ConfigsModels.Rabbit.Base;
using Octopus.Core.Common.DynamicObject.Services;
using Octopus.Core.Common.DynamicObject.Services.Interfaces;
using Octopus.Core.Common.Helpers.JsonDeserializer;
using Octopus.Core.Parser.BusinessLogic.Services;
using Octopus.Core.Parser.WorkerService.Configs.Implementations;
using Octopus.Core.Parser.WorkerService.Configuration.Implementations;
using Octopus.Core.Parser.WorkerService.Interfaces.Services;
using Octopus.Core.Parser.WorkerService.Services;
using Octopus.Core.RabbitMq.Context;
using Octopus.Core.RabbitMq.Services;
using Octopus.Core.RabbitMq.Services.Interfaces;
using System.Collections.Generic;

namespace Octopus.Core.Parser.WorkerService
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
                    confBuilder.AddJsonFile("Configs/Production/processor-config.json");
                    confBuilder.AddJsonFile("Configs/Production/Parsers/csvParser-config.json");
                    confBuilder.AddJsonFile("Configs/Production/Parsers/jsonParser-config.json");
                    confBuilder.AddJsonFile("Configs/Production/Parsers/xmlParser-config.json");
                    confBuilder.AddJsonFile("Configs/Production/rabbitMq-config.json");
                    confBuilder.AddJsonFile("Configs/Production/rabbitMqConnectionString-config.json");
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = hostContext.Configuration;
                    
                    services.Configure<ProcessorConfiguration>(configuration.GetSection(nameof(ProcessorConfiguration)));

                    services.Configure<ConnectionConfiguration>(hostContext.Configuration.GetSection("RabbitMqConnectionString"));
                    services.Configure<PublisherConfiguration>(hostContext.Configuration.GetSection("Publisher"));

                    services.AddSingleton(hostContext.Configuration.GetSection("Subscribers").Get<IEnumerable<SubscriberConfiguration>>());

                    services.Configure<CsvParserConfiguration>(configuration.GetSection(nameof(CsvParserConfiguration)));
                    services.Configure<JsonParserConfiguration>(configuration.GetSection(nameof(JsonParserConfiguration)));
                    services.Configure<XmlParserConfiguration>(configuration.GetSection(nameof(XmlParserConfiguration)));

                    services.AddSingleton<IRabbitMqContext, RabbitMqContext>();
                    services.AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>();
                    services.AddSingleton<IRabbitMqSubscriber, RabbitMqSubscriber>();
                    services.AddSingleton<IEventProcessor, MessageHandler>();

                    services.AddSingleton<IParserProcessor, ParserProcessor>();
                    services.AddSingleton<IDynamicObjectCreateService, DynamicObjectCreateService>();
                    services.AddSingleton<IJsonDeserializer, JsonDeserializer>();
                    services.AddSingleton<IDynamicTypeFactory, DynamicTypeFactory>();

                    services.AddHostedService<ParserService>();
                });
    }
}
