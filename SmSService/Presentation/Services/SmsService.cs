using System.Text;
using System.Text.Json;

namespace SmSService.Services;

public class SmsService(IConfiguration configuration, HttpClient httpClient) : ISmsService
{
    public async Task<bool> SendConfirmationSmsAsync(
        string phoneNumber,
        string firstName,
        string bookingId,
        string eventLocation,
        string eventTime,
        string eventName,
        string trainerName
    )
    {
        try
        {
            if (phoneNumber == null || string.IsNullOrWhiteSpace(phoneNumber))
            {
                return false;
            }

            var message =
                $"Hi {firstName}! Booking confirmed (ID: {bookingId}). {eventName} at {eventLocation}, {eventTime}. See you there! - {trainerName}";

            // Clean phone number (remove + prefix if present)
            var cleanPhoneNumber = phoneNumber.TrimStart('+');

            // Prepare Infobip SMS request (exact same format as working curl)
            var smsRequest = new
            {
                messages = new[]
                {
                    new
                    {
                        destinations = new[] { new { to = cleanPhoneNumber } },
                        from = configuration["FromPhoneNumber"],
                        text = message,
                    },
                },
            };

            var jsonContent = JsonSerializer.Serialize(smsRequest);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Set headers exactly like working curl
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://api.infobip.com/sms/2/text/advanced"
            )
            {
                Content = content,
            };
            request.Headers.Add("Authorization", $"App {configuration["InfobipApiKey"]}");
            request.Headers.Add("Accept", "application/json");

            Console.WriteLine($"JSON: {jsonContent}");
            Console.WriteLine("Sending SMS via Infobip...");

            // Send SMS via Infobip API
            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"SMS sent successfully to {phoneNumber}");
                Console.WriteLine($"Infobip response: {responseContent}");
                return true;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Failed to send SMS: {response.StatusCode}");
                Console.WriteLine($"Error: {errorContent}");
                return false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception occurred: {e.Message}");
            Console.WriteLine($"Stack trace: {e.StackTrace}");
            return false;
        }
    }
}
