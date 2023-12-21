using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using RifasOnline.Models.DTO;



namespace RifasOnline.Servicios.Implementacion
{
    public static class CorreoServicio
    {
        private static string _Host = "localhost";
        private static int _Puerto = 587;

        private static string _NombreEnvia = "localhost";
        private static string _Correo = "localhost";
        private static string _Clave = "localhost";

        public static bool Enviar(CorreoDTO correoDto)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(_NombreEnvia,_Correo));
                email.To.Add(MailboxAddress.Parse(correoDto.Destinatario));
                email.Subject = correoDto.Asunto;
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = correoDto.Contenido
                };

                var smtp = new SmtpClient();
                smtp.Connect(_Host, _Puerto,SecureSocketOptions.StartTls);
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
