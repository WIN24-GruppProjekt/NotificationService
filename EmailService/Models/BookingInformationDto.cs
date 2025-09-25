namespace EmailService.Models;

public class BookingInformationDto
{
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string BookingId { get; set; } = null!;
    public string EventLocation { get; set; } = null!;
    public string EventTime { get; set; } = null!;
    public string EventName { get; set; } = null!;
    public string TrainerName { get; set; } = null!;
}