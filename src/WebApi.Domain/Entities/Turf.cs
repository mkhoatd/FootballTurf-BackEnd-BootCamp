using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Domain.Enum;

namespace WebApi.Domain.Entities;
public class Turf : BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public TurfStatus Status { get; set; }
    public Guid OwnerId { get; set; }
    public User Owner { get; set; }
    public string Longitude { get; set; }
    public string Latitude { get; set; }
    public TurfType Type { get; set; }
    public int Rating { get; set; }
    public HashSet<Schedule> Schedules { get; set; }
    public HashSet<TurfImage> TurfImages { get; set; }

}