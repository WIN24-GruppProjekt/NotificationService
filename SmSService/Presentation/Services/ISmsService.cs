namespace SmSService.Services;

public interface ISmsService
{
    Task<bool> SendConfirmationSmsAsync(
        string phoneNumber,
        string firstName,
        string bookingId,
        string eventLocation,
        string eventTime,
        string eventName,
        string trainerName
    );
}
