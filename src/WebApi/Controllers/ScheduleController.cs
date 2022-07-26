using GenericBizRunner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.BusinessLogic.Schedules.Interfaces;
using WebApi.Domain.Common;
using WebApi.Domain.Entities;
using WebApi.Interfaces;
using WebApi.Repository.Interface;

namespace WebApi.Controllers
{
    public class ScheduleController : BaseApiController
    {
        private readonly ILogger<ScheduleController> _logger;

        public ScheduleController(
            ILogger<ScheduleController> logger)

        {
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet()]
        public async Task<ActionResult<List<Schedule>>> GetScheduleInAMonthAsync(Guid turfId,
           [FromServices] IActionServiceAsync<IGetScheduleInAMonthAsync> service)
        {
            var scheduleDto = await service.RunBizActionAsync<List<Schedule>>(turfId);
            if (service.Status.HasErrors)
            {
                foreach (var error in service.Status.Errors)
                {
                    var properties = error.ErrorResult.MemberNames.ToList();
                    ModelState.AddModelError(properties.Any() ? properties.First() : "", error.ErrorResult.ErrorMessage);
                }
                return BadRequest(ModelState);

            }

            _logger.LogInformation(SuccessMessage.GetScheduleInAMonthSuccess);
            return Ok(scheduleDto);
        }
    }
}