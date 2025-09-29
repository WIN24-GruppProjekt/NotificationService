namespace Presentation.Models;

public class UserBookingDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;
}
