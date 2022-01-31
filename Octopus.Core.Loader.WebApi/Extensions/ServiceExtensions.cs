using Microsoft.Extensions.DependencyInjection;

namespace Octopus.Core.Loader.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen();
        }
    }
}
