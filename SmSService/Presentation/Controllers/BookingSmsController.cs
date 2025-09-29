using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using SmSService.Services;

namespace SmSService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingSmsController(ISmsService smsService) : ControllerBase
{
    [HttpPost("confirmation")]
    public async Task<IActionResult> SendSms([FromBody] BookingInformationDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { Error = "All fields are required" });

        var result = await smsService.SendConfirmationSmsAsync(
            dto.PhoneNumber,
            dto.FirstName,
            dto.BookingId,
            dto.EventLocation,
            dto.EventTime,
            dto.EventName,
            dto.TrainerName
        );

        return result
            ? Ok("Confirmation SMS Sent")
            : StatusCode(500, "Server Error while sending the confirmation SMS");
    }
}
