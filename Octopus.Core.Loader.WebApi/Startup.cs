using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Octopus.Core.Loader.WebApi.Extensions;
using Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Context;
using Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Interfaces;
using Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Repositories;

namespace Octopus.Core.Loader.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerExtension();
            services.AddHelpersServicesExtension();
            services.AddDynamicEntityServicesExtension();
            services.AddDataBaseServicesExtension();
            services.AddRabbitMqServicesExtension();
            services.AddMongoDbServicesExtension();
        }
         
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSwaggerExtension();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
