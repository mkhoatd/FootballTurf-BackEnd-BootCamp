using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.BusinessLogic.Users.DTOs;
using WebApi.BusinessServices.UsersServices;
using WebApi.Domain.Common;
using WebApi.Domain.Entities;
using WebApi.Repository.Interface;
using WebApi.Repository.Service;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
    [Route("Api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly RegisterUserService _registerUserService;
        private readonly LoginService _loginService;

        public UsersController(
            ILogger<UsersController> logger,
            RegisterUserService registerUserService,
            LoginService loginService
            )
        {
            _logger = logger;
            _registerUserService = registerUserService;
            _loginService = loginService;
        }

        // [HttpPost()]
        // public async Task<ActionResult> CreateUserAsync()
        // {
        //     var user = (User)HttpContext.Items["User"];
        //
        //     _logger.LogInformation(SuccessMessage.CreateUserSuccess);
        //     return Ok(new ApiResponse(
        //         StatusCodes.Status200OK,
        //         SuccessMessage.CreateUserSuccess,
        //         null));
        // }
        [HttpPost()]
        public async Task<ActionResult<UserLoginDto>> RegisterAsync(LoginOrRegisterDto loginOrRegisterDto)
        {
            var userLoginDto= await _registerUserService.RegisterAsync(loginOrRegisterDto);
            if (_registerUserService.Errors.Any())
            {
                foreach (var error in _registerUserService.Errors)
                {
                    var properties = error.MemberNames.ToList();
                    ModelState.AddModelError(properties.Any()?properties.First() : "", error.ErrorMessage);
                }
                return BadRequest(ModelState);

            }

            return Ok(userLoginDto);
        }

        [HttpPost()]
        public async Task<ActionResult<UserLoginDto>> LoginAsync(LoginOrRegisterDto dto)
        {
            var userLoginDto = await _loginService.LoginAsync(dto);
            if (_loginService.Errors.Any())
            {
                foreach (var error in _loginService.Errors)
                {
                    var properties = error.MemberNames.ToList();
                    ModelState.AddModelError(properties.Any() ? properties.First() : "", error.ErrorMessage);
                }
                return BadRequest(ModelState);

            }
            return Ok(userLoginDto);
        }
    }
}