using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Common;
using WebApi.Domain.Entities;
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

        public UsersController(UserManager<User> userManager,
            ILogger<UsersController> logger
            )
        {
            _userManager = userManager;
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
    }
}