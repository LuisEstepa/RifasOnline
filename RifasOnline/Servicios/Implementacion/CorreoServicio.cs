using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using RifasOnline.Models.DTO;
using RifasOnline.Servicios.Contrato;



namespace RifasOnline.Servicios.Implementacion
{
    public class CorreoServicio : ICorreoService
    {
        private static string _Host = "smtp.gmail.com";
        private static int _Puerto = 587;
        private static string _NombreEnvia = "luis.estepa2021@gmail.com";
        private static string _Correo = "luis.estepa2021@gmail.com";
        private static string _Clave = "vkjpvnrlyrlhvplr";

        public bool EnviarCorreo(CorreoDTO correoDto)
        {
            try
            {
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress(_NombreEnvia, _Correo));
                email.To.Add(MailboxAddress.Parse(correoDto.Destinatario));
                email.Subject = correoDto.Asunto;
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = correoDto.Contenido
                };
                var smtp = new SmtpClient();
                smtp.Connect(_Host, _Puerto, SecureSocketOptions.StartTls);

                smtp.Authenticate(_Correo, _Clave);
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