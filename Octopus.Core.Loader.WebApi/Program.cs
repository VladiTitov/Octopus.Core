using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Common.ConfigsModels.Rabbit;
using Octopus.Core.Common.ConfigsModels.Rabbit.Base;

namespace Octopus.Core.Loader.WebApi
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
                    confBuilder.AddJsonFile("Configs/configures.json");
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<RabbitMqConfiguration>(hostContext.Configuration.GetSection("RabbitParams"));
                    services.Configure<ConnectionStringConfig>(hostContext.Configuration.GetSection("ConnectionString"));
                    services.Configure<ConnectionConfiguration>(hostContext.Configuration.GetSection("RabbitMqConnectionString"));
                    services.Configure<PublisherConfiguration>(hostContext.Configuration.GetSection("Publisher"));

                    services.AddSingleton(hostContext.Configuration.GetSection("Subscribers").Get<IEnumerable<SubscriberConfiguration>>());
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
