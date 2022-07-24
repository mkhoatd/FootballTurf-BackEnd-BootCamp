using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Domain.Enum;

namespace WebApi.Domain.Entities;
public class Schedule : BaseEntity
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public ScheduleStatus Status { get; set; }
    public Guid TurfId { get; set; }
    public Turf Turf { get; set; }
}