using Application.Features;
using Microsoft.AspNetCore.Identity;
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
            services.AddScoped<TokenFeature>();
            services.AddScoped<PasswordFeature>();
            services.AddScoped<AuthFeature>();
            services.AddScoped<IPasswordHasher<object>, PasswordHasher<object>>();
            services.AddScoped<ISieveProcessor, SieveProcessor>();
            return services;
        }
    }
}
