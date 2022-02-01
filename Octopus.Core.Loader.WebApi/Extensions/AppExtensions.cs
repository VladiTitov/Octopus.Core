using Microsoft.AspNetCore.Builder;

namespace Octopus.Core.Loader.WebApi.Extensions
{
    public static class AppExtensions
    {
        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Octopus.Core.Loader.WebApi.Api");
            });
        }
    }
}
