using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Octopus.Core.Parser.Api.Configuration.ExceptionPage;
using Octopus.Core.Parser.Api.Configuration.Ioc;
using Octopus.Core.Parser.Api.Configuration.Swagger;

namespace Octopus.Core.Parser.Api
{
    public class Startup
    {
        private IConfiguration _configuration;
        private IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvcCore()
                .AddApiExplorer();

            services.ConfigureServices(_configuration, _environment);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app
                .ConfigureExceptionHandling(env)
                .ConfigureSwagger(_configuration)
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
    }
}
