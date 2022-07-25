namespace WebApi.Repository.DTOs;

public class ScheduleDto
{
    public Guid Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Status { get; set; }
    public string Title => CustomerName + " - " + CustomerPhoneNumber + " - " + Status;
    public Guid TurfId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerPhoneNumber { get; set; }
}