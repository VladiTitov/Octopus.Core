using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Octopus.Core.Loader.WebApi.Configuration.Swagger
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
            => services
                .AddSwaggerGen();

        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app)
            =>
                app
                    .UseSwagger()
                    .UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Octopus.Core.Loader.WebApi.Api");
                    });
    }
}

