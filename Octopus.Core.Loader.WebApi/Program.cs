using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Octopus.Core.Loader.WebApi.Extensions;

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
                .ConfigureAppConfiguration((hostContext, confBuilder) =>
                {
                    confBuilder.AddJsonFileExtension(hostContext);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.ConfigureExtension(hostContext);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
