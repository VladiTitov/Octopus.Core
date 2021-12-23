using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Octopus.Core.Common.Configs;
using Octopus.Core.Loader.BusinessLogic.Services;
using Octopus.Core.RabbitMq.Context;
using Octopus.Core.RabbitMq.Services.Implementations;
using Octopus.Core.RabbitMq.Services.Interfaces;

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
                    services.AddSingleton<JobRegistryService>();

                    services.Configure<RabbitMqConfiguration>(hostContext.Configuration.GetSection("RabbitParams"));

                    services.AddSingleton<IRabbitMqContext, RabbitMqContext>();
                    services.AddSingleton<IRabbitMqListener, RabbitMqListener>();

                    services.AddHostedService<Worker>();
                });
    }
}