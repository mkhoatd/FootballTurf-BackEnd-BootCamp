using WebApi.Domain.Entities;

namespace WebApi.Repository.DTOs
{
    public class MainTurfDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<string> ImageLink { get; set; }
        public Guid OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhoneNumber { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public List<Turf> Turfs { get; set; }
    }
}
