using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Octopus.Core.Parser.WorkerService.Configs.Implementations;
using Octopus.Core.Parser.WorkerService.Configuration.Implementations;
using Octopus.Core.Parser.WorkerService.Interfaces.Services;
using Octopus.Core.Parser.WorkerService.Services;

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

                    services.AddHostedService<Worker>();
                });
    }
}
