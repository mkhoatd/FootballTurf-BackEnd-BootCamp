using System.Reflection;
using GenericBizRunner.Configuration;
using WebApi.BusinessLogic.Users;
using WebApi.BusinessLogic.Users.DTOs;
using WebApi.BusinessLogic.Users.Interfaces;
using WebApi.Persistence;
using WebApi.Repository.Implementation;
using WebApi.Repository.Interface;
using WebApi.Repository.Service;

namespace WebApi.Extensions;

public static class RepositoryServiceExtension
{
    public static IServiceCollection AddRepositoryService(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}