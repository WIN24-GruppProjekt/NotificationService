# NotificationService

## EmailService
Minimal ASP.NET Core API that sends booking confirmation emails via Azure Communication Services (ACS).

## Requirements
- .NET 8 SDK
- ACS Email enabled
- Verified sender address in ACS

## API
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

## Notes
- Do not commit secrets; keep them in environment variables.
