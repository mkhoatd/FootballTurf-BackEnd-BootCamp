using WebApi.Domain.Enum;

namespace WebApi.BusinessLogic.Schedules.DTOs
{
    public class UpdateScheduleDto
    {
        public Guid ScheduleId { get; set; }
        public string Status { get; set; }
    }
}
