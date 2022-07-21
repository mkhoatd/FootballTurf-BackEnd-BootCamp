using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Infrastructure.Extensions
{
    public static class CorsServiceExtension
    {
        public static IServiceCollection AddCorsService(this IServiceCollection services, IConfiguration config)
        {
            var allowedOrigins = config.GetSection("AllowedOrigins")?.GetChildren()?.Select(x => x.Value)?.ToArray<string>();

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicyHttpAndHttps", policy =>
                {
                    policy
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins(allowedOrigins);
                });
            });

            return services;
        }
    }
}
