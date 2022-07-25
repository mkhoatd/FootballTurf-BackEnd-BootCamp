namespace WebApi.Domain.Entities;

public class MainTurf : BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public Guid OwnerId { get; set; }
    public User Owner { get; set; }
    public string Longitude { get; set; }
    public string Latitude { get; set; }
    public HashSet<Turf> Turfs { get; set; }
    
}