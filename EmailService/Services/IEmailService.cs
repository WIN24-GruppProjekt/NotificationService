namespace EmailService.Services;

public interface IEmailService
{
    Task<bool> SendConfirmationMailAsync(string email, string firstName, string bookingId, string eventLocation, string eventTime, string eventName, string trainerName);
}