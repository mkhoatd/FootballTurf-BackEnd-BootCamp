using System.ComponentModel.DataAnnotations;
using GenericBizRunner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.BusinessLogic.Users.DTOs;
using WebApi.Domain.Common;
using WebApi.Domain.Entities;
using WebApi.Repository.Interface;
using WebApi.Repository.Service;
using Microsoft.AspNetCore.Mvc;
using WebApi.BusinessLogic.Users;
using WebApi.BusinessLogic.Users.Interfaces;


namespace WebApi.Controllers
{
    [Route("Api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            ILogger<UsersController> logger
        )
        {
            _logger = logger;
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
        public async Task<ActionResult<UserLoginDto>> RegisterAsync(LoginOrRegisterDto loginOrRegisterDto,
            [FromServices]IActionServiceAsync<IRegisterUserActionAsync> service)
        {
            var userLoginDto = await service.RunBizActionAsync<UserLoginDto>(loginOrRegisterDto);
            if (service.Status.HasErrors)
            {
                foreach (var error in service.Status.Errors)
                {
                    var properties = error.ErrorResult.MemberNames.ToList();
                    ModelState.AddModelError(properties.Any()?properties.First() : "", error.ErrorResult.ErrorMessage);
                }
                return BadRequest(ModelState);

            }

            return Ok(userLoginDto);
        }

        [HttpPost()]
        public async Task<ActionResult<UserLoginDto>> LoginAsync(LoginOrRegisterDto dto,
            [FromServices]IActionServiceAsync<ILoginActionAsync> service)
        {
            var userLoginDto = await service.RunBizActionAsync<UserLoginDto>(dto);
            if (service.Status.HasErrors)
            {
                foreach (var error in service.Status.Errors)
                {
                    var properties = error.ErrorResult.MemberNames.ToList();
                    ModelState.AddModelError(properties.Any()?properties.First() : "", error.ErrorResult.ErrorMessage);
                }
                return BadRequest(ModelState);

            }
            return Ok(userLoginDto);
        }
        
    }
}