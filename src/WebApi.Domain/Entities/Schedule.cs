using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Domain.Entities;
public class Schedule : BaseEntity
{
    public int ScheduleIndex { get; set; }
    public DateTime StartTime
    {
        get
        {
            var startTime = DateTime.Today; //ngay hom nay, voi gio phut giay =0
            return startTime.AddMinutes((ScheduleIndex) * 30);
        }
    }
    public DateTime EndTime
    {
        get
        {
            return StartTime.AddMinutes(30);
        }
    }
    public Guid TurfId { get; set; }
    public Turf Turf { get; set; }
    public Guid CustomerId { get; set; }
    public User Customer { get; set; }
}