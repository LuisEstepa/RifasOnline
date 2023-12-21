using Microsoft.EntityFrameworkCore;
using RifasOnline.Models.Entities;

namespace RifasOnline.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuario(string correo, string clave);
        Task<Usuario> SaveUsuario(Usuario modelo);
        Task<bool>  UpdateUsuario(Usuario modelo);

    }
}
