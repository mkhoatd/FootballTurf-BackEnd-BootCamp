using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Enum;

namespace WebApi.Repository.DTOs
{
    public class SearchTurfDto
    {
        public string? Name { get; set; }
        public TurfType? TurfType { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
