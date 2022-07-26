using GenericBizRunner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.BusinessLogic.MainTurfs.Interfaces;
using WebApi.Domain.Entities;
using WebApi.Interfaces;
using WebApi.Repository.DTOs;

namespace WebApi.Controllers
{
    public class MainTurfsController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet()]
        public async Task<ActionResult<List<MainTurfDto>>> GetMainTurfList(
            [FromServices]IActionServiceAsync<IGetAllMainTurfActionAsync> service)
        {
            var mainTurfs = await service.RunBizActionAsync<List<MainTurfDto>>();
            if (service.Status.HasErrors)
            {
                foreach (var error in service.Status.Errors)
                {
                    var properties = error.ErrorResult.MemberNames.ToList();
                    ModelState.AddModelError(properties.Any()?properties.First() : "", error.ErrorResult.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            return Ok(mainTurfs);
        }


        [HttpGet()]
        public async Task<ActionResult<MainTurfDto>> GetMainTurfById(
            [FromServices]IActionServiceAsync<IGetMainTurfByIdActionAsync>service)
        {
            var currentUser = (User)HttpContext.Items["User"];
            if (currentUser == null)
            {
                ModelState.AddModelError("", "User doesn't exist");
                return BadRequest(ModelState);
            }

            var mainTurf = await service.RunBizActionAsync<List<MainTurfDto>>(currentUser.Id);
            if (service.Status.HasErrors)
            {
                foreach (var error in service.Status.Errors)
                {
                    var properties = error.ErrorResult.MemberNames.ToList();
                    ModelState.AddModelError(properties.Any()?properties.First() : "", error.ErrorResult.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            return Ok(mainTurf);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<List<TurfDto>>> SearchMainTurf(SearchTurfDto searchMainTurfDto,
        [FromServices] IActionServiceAsync<ISearchMainTurf> service)
        {
            var turfs = await service.RunBizActionAsync<List<MainTurfDto>>(searchMainTurfDto);
            if (service.Status.HasErrors)
            {
                foreach (var error in service.Status.Errors)
                {
                    var properties = error.ErrorResult.MemberNames.ToList();
                    ModelState.AddModelError(properties.Any() ? properties.First() : "", error.ErrorResult.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            return Ok(turfs);
        }
    }
}
