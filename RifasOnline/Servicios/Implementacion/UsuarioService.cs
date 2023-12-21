using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RifasOnline.Models;
using RifasOnline.Models.Entities;
using RifasOnline.Servicios.Contrato;

namespace RifasOnline.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly DbRifasOnlineContext _Context;
        public UsuarioService(DbRifasOnlineContext dbContext)
        {
            _Context = dbContext;
        }

        public async Task<bool> GetEmailUsuario(string correo)
        {
            Usuario usuario_encontrado = await _Context.Usuarios.Where(u => u.Correo == correo)
                 .FirstOrDefaultAsync();

            if (usuario_encontrado != null)
            
                return true;
            else 
                return false;            
                
        }

        public async Task<Usuario> GetUsuario(string correo, string clave)
        {
            Usuario usuario_encontrado = await _Context.Usuarios.Where(u => u.Correo == correo && u.Clave == clave)
                 .FirstOrDefaultAsync();

            return usuario_encontrado;
        }

        public async Task<Usuario> SaveUsuario(Usuario modelo)
        {
            _Context.Usuarios.Add(modelo);
            await _Context.SaveChangesAsync();
            return modelo;
        }

        public async Task<bool> UpdateUsuario(Usuario modelo)
        {
            Usuario UsuarioActualizar = await _Context.Usuarios.Where(l => l.Token == modelo.Token).FirstOrDefaultAsync();
            UsuarioActualizar.Clave = modelo.Clave;
            UsuarioActualizar.Restablecer = modelo.Restablecer;
            _Context.Entry(UsuarioActualizar).State = EntityState.Modified;
             
            if(_Context.SaveChanges() == 1)
                return true;
            else 
                return false;
        }
        
    }
}
