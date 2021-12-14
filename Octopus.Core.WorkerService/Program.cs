using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Octopus.Core.Common.Configs;
using Octopus.Core.Loader.RabbitMq.Context;
using Octopus.Core.Loader.RabbitMq.Services;

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
                    services.AddSingleton<IRabbitMqListener, RabbitMqListener>();

                    services.AddHostedService<Worker>();
                });
    }
}
