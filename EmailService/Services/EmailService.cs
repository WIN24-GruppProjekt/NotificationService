using Azure;
using Azure.Communication.Email;

namespace EmailService.Services;

public class EmailService(IConfiguration configuration, EmailClient emailClient) : IEmailService
{
    public async Task<bool> SendConfirmationMailAsync(
        string email,
        string firstName,
        string bookingId,
        string eventLocation,
        string eventTime,
        string eventName,
        string trainerName)
    {

        try
        {
            if (email == null || string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            var subject = $"Your booking confirmation";
            var plainText = @$"Hi, {firstName} this is your Booking Confirmation (ID:{bookingId}) for {eventName}";
            var html = @$"<html>
			                <body style=""margin:0, auto;"">
				                <h1 style=""text-align: center;"">Booking confirmation (ID:{bookingId})</h1>
                                <p>Hi, <strong>{firstName}</strong></p>
                                <p> you have booked training session <i>{eventName} at {eventLocation} with start time at {eventTime}</i>.</p>
                                <p> Looking forward to seeing you,</p>
                                <p>{trainerName}</p>
                                <p> <strong>Core Gym AB</strong></p>
			                </body>
		                 </html>";

            var emailMessage = new EmailMessage(
                senderAddress: configuration["SenderAddress"],
                recipients: new EmailRecipients([new(email)]),
                content: new EmailContent(subject)
                {
                    PlainText = plainText,
                    Html = html
                });

            await emailClient.SendAsync(WaitUntil.Started, emailMessage);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }

    }
}