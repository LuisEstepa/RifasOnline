using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using RifasOnline.Models.DTO;
using RifasOnline.Servicios.Contrato;



namespace RifasOnline.Servicios.Implementacion
{
    public class CorreoServicio : IEmailService
    {
        private readonly IConfiguration _config;               

        public CorreoServicio(IConfiguration config)
        {
            _config = config;
        }
        
        public bool SendEmail(CorreoDTO correoDto)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(_config.GetSection("Email:UserName").Value,""));
                email.To.Add(MailboxAddress.Parse(correoDto.Destinatario));
                email.Subject = correoDto.Asunto;
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = correoDto.Contenido
                };

                var smtp = new SmtpClient();
                smtp.Connect(_config.GetSection("Email:Host").Value,
                             Convert.ToInt32(_config.GetSection("Email:Port").Value),
                             SecureSocketOptions.StartTls);
                smtp.Authenticate(_config.GetSection("Email:UserName").Value, _config.GetSection("Email:PassWord").Value);
                smtp.Send(email);
                smtp.Disconnect(true);
                
                return true;

            }
            catch
            {
                return false;
            }
        }
    }
}
