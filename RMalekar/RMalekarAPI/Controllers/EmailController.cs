using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMalekarAPI.Services;

namespace RMalekarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;

        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] ContactForm form)
        {
            if(form==null || string.IsNullOrWhiteSpace(form.Email) || string.IsNullOrWhiteSpace(form.Message))
            {
                return BadRequest("Invalid Form Data.");
            }

            var subject = " New Message from Contact Form.";
            var body = $"Recienved a new message from {form.Name} ({form.Email}):<br /> <b> {form.Subject} </b><br /><br />{form.Message}";

            try
            {
                await _emailService.SendEmailAsync("devrmalekar@gmail.com", subject, body);
                return Ok("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, ex.Message);
            }
        }
    }

    public class ContactForm
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
