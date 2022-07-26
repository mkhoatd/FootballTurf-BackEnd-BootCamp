using GenericBizRunner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.BusinessLogic.Turfs.Interfaces;
using WebApi.Interfaces;
using WebApi.Repository.DTOs;

namespace WebApi.Controllers;

public class TurfsController : BaseApiController
{
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<TurfDto>>> GetTurfsInMainTurf(string mainTurfId,
        [FromServices]IActionServiceAsync<IGetTurfsInMainTurfActionAsync>service)
    {
        var turfs = await service.RunBizActionAsync<List<TurfDto>>(mainTurfId);
        if (service.Status.HasErrors)
        {
            foreach (var error in service.Status.Errors)
            {
                var properties = error.ErrorResult.MemberNames.ToList();
                ModelState.AddModelError(properties.Any()?properties.First() : "", error.ErrorResult.ErrorMessage);
            }
            return BadRequest(ModelState);
        }

        return Ok(turfs);
    }
}