using RifasOnline.Models.DTO;

namespace RifasOnline.Servicios.Contrato
{
    public interface IEmailService
    {
        bool SendEmail(CorreoDTO correoDto);
    }
}
