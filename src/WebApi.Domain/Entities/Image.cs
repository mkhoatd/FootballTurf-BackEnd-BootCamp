using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Domain.Entities;
public class Image : BaseEntity
{
    public string Link { get; set; }
    public HashSet<TurfImage> TurfImages { get; set; }
}