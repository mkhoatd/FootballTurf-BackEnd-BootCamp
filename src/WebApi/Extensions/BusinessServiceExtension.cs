using System.Reflection;
using GenericBizRunner;
using GenericBizRunner.Configuration;
using WebApi.BusinessLogic.Users;
using WebApi.BusinessLogic.Users.DTOs;
using WebApi.BusinessLogic.Users.Interfaces;
using WebApi.Persistence;
using WebApi.Repository.Implementation;
using WebApi.Repository.Interface;
using WebApi.Repository.Service;
using NetCore.AutoRegisterDi;
using WebApi.BusinessLogic.Schedules.Interfaces;
using WebApi.BusinessLogic.Schedules;

namespace WebApi.Extensions;

public static class BusinessServiceExtension
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        
        services.RegisterBizRunnerWithDtoScans<AppFootballTurfDbContext>((Assembly)null);
        services.AddTransient<ILoginActionAsync, LoginActionAsync>();
        services.AddTransient<IRegisterUserActionAsync, RegisterUserActionAsync>();
        services.AddTransient<IGetScheduleInAMonthAsync, GetScheduleInAMonthAsync>();

        return services;
    }
    
}