using RifasOnline.Models.DTO;

namespace RifasOnline.Servicios.Contrato
{
    public interface ICorreoService
    {
        bool EnviarCorreo(CorreoDTO correoDto);
    }
}
