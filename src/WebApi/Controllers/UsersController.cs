using System.ComponentModel.DataAnnotations;
using GenericBizRunner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.BusinessLogic.Users.DTOs;
using WebApi.Domain.Common;
using WebApi.Domain.Entities;
using WebApi.Repository.Interface;
using WebApi.Repository.Service;
using WebApi.BusinessLogic.Users;
using WebApi.BusinessLogic.Users.Interfaces;
using WebApi.Interfaces;


namespace WebApi.Controllers
{

    public class UsersController : BaseApiController
    {
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            ILogger<UsersController> logger
        )
        {
            _logger = logger;
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
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