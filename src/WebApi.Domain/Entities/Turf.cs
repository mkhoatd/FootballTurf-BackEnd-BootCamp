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
    public List<Schedule> Schedules { get; set; }
    public List<Image> Images { get; set; }

}