﻿using Microsoft.Extensions.DependencyInjection;
using WebApi.Repository.Implementation;
using WebApi.Repository.Interface;
using WebApi.Repository.Service;

namespace WebApi.Infrastructure.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
