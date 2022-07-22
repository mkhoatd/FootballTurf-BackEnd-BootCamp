using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Domain.Entities;
public class TurfImage : BaseEntity
{
    public Guid TurfId { get; set; }
    public Turf Turf { get; set; }
    public Guid ImageId { get; set; }
    public Image Image { get; set; }
}