using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Octopus.Core.Parser.Api.Configuration.ExceptionPage
{
    public static class ExceptionPageConfiguration
    {
        public static IApplicationBuilder ConfigureExceptionHandling(this IApplicationBuilder app, 
            IWebHostEnvironment env)
            => env.IsDevelopment() ? app.UseDeveloperExceptionPage() : app;
    }
}
