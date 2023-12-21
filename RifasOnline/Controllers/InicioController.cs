using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using RifasOnline.Servicios.Contrato;
using RifasOnline.Recursos;
using RifasOnline.Models.Entities;
namespace RifasOnline.Controllers
{
    public class InicioController : Controller
    {
        private readonly IUsuarioService _usuarioServicio;
        public InicioController(IUsuarioService usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;            
        }

        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(Usuario modelo)
        {
            modelo.Clave = Utilidades.EncriptarClave(modelo.Clave);
            var ConfirmarClave = Utilidades.EncriptarClave(modelo.ConfirmarClave);
            modelo.Restablecer = false;
            modelo.Token = "";

            if (ConfirmarClave == modelo.Clave)
            {
                Usuario usuario_creado = await _usuarioServicio.SaveUsuario(modelo);

                if (usuario_creado.IdUsuario > 0)
                {
                    return RedirectToAction("IniciarSesion", "Inicio");
                }
                else
                {
                    ViewData["Mensaje"] = "No se pudo crear el usuario";
                }
            }
            else
            {
                ViewData["Mensaje"] = "Las contranseñas no coinciden revise!!";
            }

            return View();
        }

        public async Task<IActionResult> ActualizarClave()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarClave(int restablecer, string clave, string confirmarClave, string token)
        {

            Usuario modelo = new()
            {
                Restablecer = Convert.ToBoolean(restablecer),
                Clave = Utilidades.EncriptarClave(clave),
                ConfirmarClave = Utilidades.EncriptarClave(confirmarClave),
                Token = token
            };

            if (modelo.ConfirmarClave == modelo.Clave)
            {
                var resultado = await _usuarioServicio.UpdateUsuario(modelo);

                if (resultado == true)
                {
                    ViewData["Mensaje"] = "Credenciales de usuario actualizadas";
                    return RedirectToAction("IniciarSesion", "Inicio");
                }
                else
                {
                    ViewData["Mensaje"] = "No se pudo Actualizar el usuario";
                }
            }
            else
            {
                ViewData["Mensaje"] = "Las contranseñas no coinciden revise!!";
            }
            return View();
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string correo, string clave)
        {
            Usuario usuario_encontrado = await _usuarioServicio.GetUsuario(correo, Utilidades.EncriptarClave(clave));

            if (usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }

            List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, usuario_encontrado.NombreUsuario)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );

            return RedirectToAction("Index", "Home");
        }
    }
}
