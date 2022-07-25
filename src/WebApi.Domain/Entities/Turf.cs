using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Domain.Enum;

namespace WebApi.Domain.Entities;
public class Turf : BaseEntity
{
    public string Name { get; set; }
    public Guid MainTurfId { get; set; }
    public MainTurf MainTurf { get; set; }

    public TurfType Type { get; set; }
    public int Rating { get; set; }
    public HashSet<Schedule> Schedules { get; set; }
    public HashSet<Image> Images { get; set; }

}