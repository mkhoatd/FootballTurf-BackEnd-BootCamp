using WebApi.BusinessServices.UsersServices;
using WebApi.Repository.Implementation;
using WebApi.Repository.Interface;
using WebApi.Repository.Service;

namespace WebApi.Extensions;

public static class RepositoryServiceExtension
{
    public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<RegisterUserService>();
        services.AddScoped<LoginService>();
        return services;
    }
}