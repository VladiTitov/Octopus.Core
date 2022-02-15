using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Octopus.Core.Parser.Api.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;

namespace Octopus.Core.Parser.Api.Configuration.Swagger
{
    public static class SwaggerConfiguration
    {
        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            var appCfg = configuration.GetApplicationConfiguration();
            var url = string.Format(appCfg.SwaggerUrlTemplate, appCfg.Version);

            app.UseSwagger();
            app.UseSwaggerUI(_ => _.SwaggerEndpoint(url, appCfg.Name));

            return app;
        }

        public static IServiceCollection RegisterSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var appCfg = configuration.GetApplicationConfiguration();

            services.AddSwaggerGen(opts =>
            {
                opts.CustomSchemaIds(type => type.FullName);
                opts.SwaggerDoc(appCfg.Version, new OpenApiInfo { Title = appCfg.Name, Version = appCfg.Version });
                opts.EnableAnnotations();
                opts.AddXmlComments();
            });

            return services;
        }

        private static void AddXmlComments(this SwaggerGenOptions options)
        {
            foreach (var file in new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).EnumerateFiles("*.xml"))
            {
                options.IncludeXmlComments(file.FullName);
            }
        }
    }
}
