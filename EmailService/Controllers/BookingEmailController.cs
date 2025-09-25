using EmailService.Models;
using EmailService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmailService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingEmailController(IEmailService emailService) : ControllerBase
{
    [HttpPost("confirmation")]
    public async Task<IActionResult> SendEmail([FromBody] BookingInformationDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { Error = "All fields are required" });
        
        var result = await emailService.SendConfirmationMailAsync(dto.Email, dto.FirstName, dto.BookingId, dto.EventLocation, dto.EventTime, dto.EventName, dto.TrainerName);

        return result ? Ok("Confirmation Email Sent") : StatusCode(500, "Server Error while sending the confirmation email");
    }
}