using WebApi.Domain;
using WebApi.Domain.Entities;

namespace WebApi.Repository.DTOs;

public class ScheduleDto
{
    public string Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Status { get; set; }
    public Guid TurfId { get; set; }
    public Turf Turf { get; set; }
    public Guid CustomerId { get; set; }
    
    public User Customer { get; set; }

    public ScheduleDto(Schedule sche)
    {
        Id = sche.Id.ToString();
        Start = sche.Start;
        End = sche.End;
        Status = sche.Status.ToString();
        TurfId = sche.TurfId;
        Turf = sche.Turf;
        CustomerId = sche.CustomerId;
        Customer = sche.Customer;
    }
}