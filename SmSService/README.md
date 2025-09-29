# SMS Service - Azure Communication Services

This SMS service is built using Azure Communication Services and follows the same structure as the EmailService. It provides SMS booking confirmation functionality for Core Gym.

## Features

- Send SMS booking confirmations
- Built with Azure Communication Services SMS SDK
- RESTful API with Swagger documentation
- Similar structure to EmailService for consistency

## Prerequisites

1. **Azure Communication Services Resource**

   - Create an Azure Communication Services resource in the Azure Portal
   - Get the connection string from the resource

2. **SMS-Enabled Phone Number**
   - Purchase an SMS-enabled phone number from your Azure Communication Services resource
   - Note: SMS capabilities depend on your Azure billing address location

## Setup Instructions

### 1. Install Dependencies

```bash
cd SmSService/Presentation
dotnet restore
```

### 2. Configure Settings

Create a local configuration file `appsettings.local.json` with your Infobip API details:

```json
{
  "FromPhoneNumber": "YOUR_INFOBIP_PHONE_NUMBER",
  "InfobipApiKey": "YOUR_INFOBIP_API_KEY"
}
```

**Where to find these values:**

- **API Key**: Infobip Portal → API Keys → Create or use existing API key
- **Phone Number**: Your Infobip phone number (without + prefix)

**Important**: Never commit your real API keys to Git. The `appsettings.local.json` file is ignored by Git for security.

### 3. Run the Service

```bash
dotnet run
```

The service will be available at:

- HTTPS: `https://localhost:7041`
- HTTP: `http://localhost:5041`
- Swagger UI: `https://localhost:7041/swagger`

## API Endpoints

### Send SMS Confirmation

**POST** `/api/BookingSms/confirmation`

**Request Body:**

```json
{
  "phoneNumber": "+1234567890",
  "firstName": "John",
  "bookingId": "BK123456",
  "eventLocation": "Core Gym Downtown",
  "eventTime": "2025-09-27 10:00 AM",
  "eventName": "Personal Training Session",
  "trainerName": "Mike Johnson"
}
```

**Response:**

- **200 OK**: `"Confirmation SMS Sent"`
- **400 Bad Request**: `{"Error": "All fields are required"}`
- **500 Internal Server Error**: `"Server Error while sending the confirmation SMS"`

## SMS Message Format

The SMS message sent to customers follows this format:

```
Hi [FirstName]! Your booking confirmation (ID: [BookingId]) for [EventName] at [EventLocation] starting [EventTime]. Looking forward to seeing you! - [TrainerName], Core Gym AB
```

## Testing

Use the included `SmSService.http` file to test the API endpoints, or use the Swagger UI at `/swagger`.

## Important Notes

1. **Phone Number Format**: Use E.164 format (e.g., +1234567890)
2. **SMS Limitations**:
   - Message length is limited (typically 160 characters for single SMS)
   - Long messages are automatically split into multiple SMS
3. **Delivery Reports**: Enabled by default for tracking message delivery
4. **Error Handling**: Service returns false on any SMS sending failure

## Project Structure

```
SmSService/Presentation/
├── Controllers/
│   └── BookingSmsController.cs     # API controller for SMS operations
├── Models/
│   └── BookingInformationDto.cs    # Data transfer object
├── Services/
│   ├── ISmsService.cs              # Service interface
│   └── SmsService.cs               # SMS service implementation
├── Program.cs                      # Application configuration
├── SmSService.http                 # HTTP test requests
└── appsettings.json               # Configuration settings
```

## Dependencies

- `Azure.Communication.Sms` (v1.0.2) - Azure SMS SDK
- `Swashbuckle.AspNetCore` - Swagger documentation
- `Microsoft.AspNetCore.OpenApi` - OpenAPI support

## Related Services

This SMS service works alongside the EmailService in the NotificationService solution to provide comprehensive booking notifications.
