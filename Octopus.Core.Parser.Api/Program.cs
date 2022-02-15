using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Octopus.Core.Parser.Api.Extensions;

namespace Octopus.Core.Parser.Api
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
                    confBuilder.AddJsonFiles(hostContext);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
