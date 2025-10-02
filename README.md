# NotificationService

## EmailService
Minimal ASP.NET Core API that sends booking confirmation emails via Azure Communication Services (ACS).

### Requirements
- .NET 9
- ACS Email enabled
- Verified sender address in ACS

Before running, you need to set up environment variables (or use a secrets store). At minimum:

| Name | Description |
|------|-------------|
| `ACS_CONNECTION_STRING` | Connection string for your Azure Communication Services instance |
| `ACS_EMAIL_SENDER` | Verified “from” email address |

### API
- Method: POST
- Route: /api/BookingEmail/confirmation
- Body:
- ```json
  {
  "Email": "user@example.com",
  "FirstName": "Alex",
  "BookingId": "ABC123",
  "EventLocation": "Main Hall",
  "EventTime": "2025-10-01 10:00",
  "EventName": "Yoga Basics",
  "TrainerName": "Jamie"
  }
  ```

### Notes
- Do not commit secrets; keep them in environment variables.

### Further development

- Add other notification channels (SMS, push)
- E-mail templates
- Retry / error handling improvements
- Unit tests

### Links
- [Azure Communication Service](https://learn.microsoft.com/en-us/azure/communication-services/overview)

## SMSService

Minimal ASP.NET Core API that sends booking confirmation SMS messages via Infobip's SMS API. The service receives booking information and automatically formats and sends confirmation messages to users' mobile phones.

### Requirements

- .NET 9 SDK
- Infobip account with API access
- Valid sender phone number registered in Infobip

### Setup

1. **Get Infobip Credentials**

   - Sign up at [Infobip](https://www.infobip.com/)
   - Navigate to your account settings to get your API key
   - Register and verify a sender phone number

2. **Configure the Application**

   You have three options for storing configuration:

   **Option A: Environment Variables (Recommended for Production)**

   ```bash
   export InfobipApiKey="your-api-key-here"
   export FromPhoneNumber="your-sender-number"
   ```

   **Option B: Local Configuration File (Recommended for Development)**

   Create `appsettings.local.json` in the `SmSService/Presentation/` directory:

   ```json
   {
     "FromPhoneNumber": "YourInfobipPhoneNumber",
     "InfobipApiKey": "YourInfobipApiKey"
   }
   ```

   > ⚠️ This file is gitignored. Never commit secrets to version control.

   **Option C: User Secrets (Alternative for Development)**

   ```bash
   cd SmSService/Presentation
   dotnet user-secrets set "InfobipApiKey" "your-api-key-here"
   dotnet user-secrets set "FromPhoneNumber" "your-sender-number"
   ```

3. **Run the Service**

   ```bash
   cd SmSService/Presentation
   dotnet restore
   dotnet run
   ```

   The service will start on:

   - HTTPS: `https://localhost:7017`
   - HTTP: `http://localhost:5131`

### API

#### Send Booking Confirmation SMS

**Endpoint:** `POST /api/BookingSms/confirmation`

**Request Headers:**

```
Content-Type: application/json
```

**Request Body:**

```json
{
  "PhoneNumber": "+1234567890",
  "FirstName": "Alex",
  "BookingId": "ABC123",
  "EventLocation": "Main Hall",
  "EventTime": "2025-10-01 10:00",
  "EventName": "Yoga Basics",
  "TrainerName": "Jamie"
}
```

**Field Descriptions:**

| Field         | Type   | Required | Description                                |
| ------------- | ------ | -------- | ------------------------------------------ |
| PhoneNumber   | string | Yes      | Recipient's phone number with country code |
| FirstName     | string | Yes      | Recipient's first name                     |
| BookingId     | string | Yes      | Unique booking identifier                  |
| EventLocation | string | Yes      | Location where the event takes place       |
| EventTime     | string | Yes      | Date and time of the event                 |
| EventName     | string | Yes      | Name of the booked event                   |
| TrainerName   | string | Yes      | Name of the trainer/instructor             |

**Success Response:**

```json
Status: 200 OK
"Confirmation SMS Sent"
```

**Error Responses:**

```json
Status: 400 Bad Request
{
  "Error": "All fields are required"
}
```

```json
Status: 500 Internal Server Error
"Server Error while sending the confirmation SMS"
```

### Testing

**Using curl:**

```bash
curl -X POST https://localhost:7017/api/BookingSms/confirmation \
  -H "Content-Type: application/json" \
  -d '{
    "PhoneNumber": "+1234567890",
    "FirstName": "Alex",
    "BookingId": "ABC123",
    "EventLocation": "Main Hall",
    "EventTime": "2025-10-01 10:00",
    "EventName": "Yoga Basics",
    "TrainerName": "Jamie"
  }'
```

**Using the included .http file:**

Open `SmSService/Presentation/SmSService.http` in your IDE (VS Code with REST Client extension or Visual Studio) and execute the request.

**Swagger UI:**

Navigate to `https://localhost:7017/swagger` when the service is running to use the interactive API documentation.

### SMS Message Format

The service automatically formats the confirmation message as:

```
Hi {FirstName}! Booking confirmed (ID: {BookingId}). {EventName} at {EventLocation}, {EventTime}. See you there! - {TrainerName}
```

Example:

```
Hi Alex! Booking confirmed (ID: ABC123). Yoga Basics at Main Hall, 2025-10-01 10:00. See you there! - Jamie
```

### Project Structure

```
SmSService/
└── Presentation/
    ├── Controllers/
    │   └── BookingSmsController.cs    # API endpoint handling
    ├── Services/
    │   ├── ISmsService.cs             # Service interface
    │   └── SmsService.cs              # Infobip integration logic
    ├── Models/
    │   └── BookingInformationDto.cs   # Request model
    ├── Program.cs                      # Application configuration
    └── appsettings.json                # Configuration file
```

### Notes

- **Security:** Never commit secrets to version control. Use environment variables, user secrets, or Azure Key Vault in production.
- **Phone Number Format:** Phone numbers should include the country code. The service accepts numbers with or without the `+` prefix (e.g., `+1234567890` or `1234567890`).
- **Rate Limiting:** Be aware of Infobip's rate limits for your account tier.
- **Cost:** SMS messages incur costs based on your Infobip pricing plan.
- **CORS:** The service is configured to allow all origins in development. Restrict this in production.

### Troubleshooting

**Problem:** SMS not sending

- Verify your Infobip API key is correct
- Check that your sender phone number is verified in Infobip
- Ensure recipient phone number includes country code
- Check console logs for detailed error messages from Infobip API

**Problem:** 400 Bad Request

- Verify all required fields are included in the request body
- Check that Content-Type header is set to `application/json`

### Further Development

- Message templates system for different notification types
- Queue-based message processing for high volume
- Retry logic with exponential backoff
- Database logging of sent messages
- Unit and integration tests
- Support for multiple SMS providers (Twilio, AWS SNS, etc.)
- Delivery status webhooks
- Message scheduling
- Internationalization support

### Links

- [Infobip SMS API Documentation](https://www.infobip.com/docs/api/channels/sms)
- [Infobip Developer Portal](https://dev.infobip.com/)
- [.NET 9 Documentation](https://learn.microsoft.com/en-us/dotnet/)
