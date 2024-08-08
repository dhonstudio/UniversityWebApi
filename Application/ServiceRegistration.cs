using Application.Features;
using Microsoft.Extensions.DependencyInjection;
using Sieve.Services;

namespace Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<CourseFeature>();
            services.AddScoped<StudentFeature>();
            services.AddScoped<ISieveProcessor, SieveProcessor>();
            return services;
        }
    }
}
