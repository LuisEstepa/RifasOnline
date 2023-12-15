using Microsoft.EntityFrameworkCore;
using RifasOnline.Models;

namespace RifasOnline.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuario(string correo, string clave);
        Task<Usuario> SaveUsuario(Usuario modelo);

    }
}
