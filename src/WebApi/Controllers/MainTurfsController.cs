using GenericBizRunner;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.BusinessLogic.MainTurfs.Interfaces;
using WebApi.Interfaces;
using WebApi.Repository.DTOs;

namespace WebApi.Controllers
{
    public class MainTurfsController : BaseApiController
    {
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
        public async Task<ActionResult<MainTurfDto>> GetMainTurfById(string id,
            [FromServices]IActionServiceAsync<IGetMainTurfByIdActionAsync>service)
        {
            var mainTurf = await service.RunBizActionAsync<MainTurfDto>(id);
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
    }
}
