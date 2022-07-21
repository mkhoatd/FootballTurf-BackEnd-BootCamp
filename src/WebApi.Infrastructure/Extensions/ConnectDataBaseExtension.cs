
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Persistence;

namespace WebApi.Infrastructure.Extensions
{
    public static class ConnectDataBaseExtension
    {
        public static IServiceCollection ConnectDBService(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppFootballTurfDbContext>(options =>
            {
                string connectionString = config.GetConnectionString("FootballTurfDB");

                options.UseNpgsql(connectionString, b => b.MigrationsAssembly("WebApi.Persistence"));
            });

            return services;
        }
    }
}
