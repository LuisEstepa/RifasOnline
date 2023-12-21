using Microsoft.AspNetCore.Mvc;
using RifasOnline.Models.DTO;
using RifasOnline.Servicios.Contrato;

namespace RifasOnline.Controllers
{
    [Route("api/(controller)")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult SendEmail(CorreoDTO correoDto)
        {
            _emailService.SendEmail(correoDto);
            return Ok();
        }
    }
}
