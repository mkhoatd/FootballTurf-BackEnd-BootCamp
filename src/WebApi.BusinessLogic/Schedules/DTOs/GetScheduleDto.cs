using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApi.BusinessLogic.Schedules.DTOs
{
    public class GetScheduleDto
    {
        [JsonPropertyName("turfId")]
        public Guid TurfId { get; set; }
    }
}
