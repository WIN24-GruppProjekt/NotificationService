namespace Presentation.Models;

public class BookingInformationDto
{
    public string PhoneNumber { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string BookingId { get; set; } = null!;
    public string EventLocation { get; set; } = null!;
    public string EventTime { get; set; } = null!;
    public string EventName { get; set; } = null!;
    public string TrainerName { get; set; } = null!;
}
