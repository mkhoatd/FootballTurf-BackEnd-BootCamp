using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Domain.Enum;

namespace WebApi.Domain.Entities;
public class Schedule : BaseEntity
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public ScheduleStatus Status { get; set; }
    public Guid TurfId { get; set; }
    public Turf Turf { get; set; }
    public Guid CustomerId { get; set; }
    
    public User Customer { get; set; }
}