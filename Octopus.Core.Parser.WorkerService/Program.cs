using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Octopus.Core.Common.Helpers.JsonDeserializer;
using Octopus.Core.Parser.WorkerService.Configs.Implementations;
using Octopus.Core.Parser.WorkerService.Configuration.Implementations;
using Octopus.Core.Parser.WorkerService.Interfaces.Services;
using Octopus.Core.Parser.WorkerService.Interfaces.Services.DynamicModels;
using Octopus.Core.Parser.WorkerService.Services;
using Octopus.Core.Parser.WorkerService.Services.DynamicModels;

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
                    confBuilder.AddJsonFile("Configs/processor-config.json");
                    confBuilder.AddJsonFile("Configs/Parsers/csvParser-config.json");
                    confBuilder.AddJsonFile("Configs/Parsers/jsonParser-config.json");
                    confBuilder.AddJsonFile("Configs/Parsers/xmlParser-config.json");
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = hostContext.Configuration;
                    
                    services.Configure<ProcessorConfiguration>(configuration.GetSection(nameof(ProcessorConfiguration)));

                    services.Configure<CsvParserConfiguration>(configuration.GetSection(nameof(CsvParserConfiguration)));
                    services.Configure<JsonParserConfiguration>(configuration.GetSection(nameof(JsonParserConfiguration)));
                    services.Configure<XmlParserConfiguration>(configuration.GetSection(nameof(XmlParserConfiguration)));

                    services.AddSingleton<IQueueConsumer, QueueConsumer>();
                    services.AddSingleton<IParserProcessor, ParserProcessor>();
                    services.AddSingleton<IDynamicObjectCreateService, DynamicObjectCreateService>();
                    services.AddSingleton<IJsonDeserializer, JsonDeserializer>();
                    services.AddSingleton<IDynamicTypeFactory, DynamicTypeFactory>();

                    services.AddHostedService<Worker>();
                });
    }
}
