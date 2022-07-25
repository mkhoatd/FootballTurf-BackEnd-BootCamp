using GenericBizRunner;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.BusinessLogic.MainTurfs;
using WebApi.Interfaces;
using WebApi.Repository.DTOs;
using WebApi.BusinessLogic.MainTurfs.Interfaces;

namespace WebApi.Controllers
{
    public class TurfsController : BaseApiController
    {
        [HttpGet()]
        public async Task<ActionResult<MainTurfDto>> GetMainTurfList(
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
    }
}
