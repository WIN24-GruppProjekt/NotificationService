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

about sms service....
