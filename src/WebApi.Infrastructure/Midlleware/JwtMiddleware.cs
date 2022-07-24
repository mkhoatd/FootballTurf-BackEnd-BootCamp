using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using WebApi.Repository.Helpers;
using WebApi.Repository.Interface;

namespace WebApi.Infrastructure.Midlleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserRepository userService, ITokenService tokenService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var userId = tokenService.ValidateJwtToken(token);
                if (userId != null)
                {
                    // attach user to context on successful jwt validation
                    context.Items["User"] = await userService.GetUserById(userId);
                }
            }
            await _next(context);
        }
    }
}
