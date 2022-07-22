using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Common;
using WebApi.Domain.Entities;
using WebApi.DTOs;
using WebApi.Hubs;
using WebApi.Repository.Interface;
using WebApi.Repository.Service;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("Api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            ILogger<UsersController> logger
            )
        {
            _logger = logger;
        }

        [HttpPost()]
        public async Task<ActionResult> CreateUserAsync()
        {
            var user = (User)HttpContext.Items["User"];

            _logger.LogInformation(SuccessMessage.CreateUserSuccess);
            return Ok(new ApiResponse(
                StatusCodes.Status200OK,
                SuccessMessage.CreateUserSuccess,
                null));
        }

        [HttpPost()]
        public async Task<ActionResult<UserDto>> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User(registerDto.Username, registerDto.Password);
            return null;
        }
    }
}