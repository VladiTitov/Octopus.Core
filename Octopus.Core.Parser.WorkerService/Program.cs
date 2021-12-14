using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddScoped<IParserProcessor, ParserProcessor>();
                    services.AddScoped<IQueueConsumer, QueueConsumer>();
                    //services.AddScoped<IBaseParser, BaseParser>();

                    services.AddHostedService<Worker>();
                });
    }
}
