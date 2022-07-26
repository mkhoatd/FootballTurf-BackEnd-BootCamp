using WebApi.Repository.Implementation;
using WebApi.Repository.Interface;
using WebApi.Repository.Service;


namespace WebApi.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IMainTurfsRepository, MainTurfsRepository>();
            services.AddScoped<ITurfsRepository, TurfsRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<IHubRepository, HubRepository>();

            return services;
        }
    }
}
