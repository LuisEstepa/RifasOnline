using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using RifasOnline.Servicios.Contrato;
using RifasOnline.Recursos;
using RifasOnline.Models.Entities;
using RifasOnline.Models.DTO;
using RifasOnline.Servicios.Implementacion;
using Microsoft.AspNetCore.Http;
using System.IO;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

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
            CorreoServicio correo = new();
            modelo.Clave = Utilidades.EncriptarClave(modelo.Clave);
            var ConfirmarClave = Utilidades.EncriptarClave(modelo.ConfirmarClave);
            modelo.Restablecer = false;
            modelo.Token = Utilidades.GenerarToken();

            if (ConfirmarClave == modelo.Clave)
            {
                if (await _usuarioServicio.GetEmailUsuario(modelo.Correo) == false)
                {
                    bool respuesta = await _usuarioServicio.SaveUsuario(modelo);

                    if (respuesta == true)
                    {

                        //string path = HttpContext.Server.MapPath("~/Plantilla/Confirmar.html");
                        //string content = System.IO.File.ReadAllText(path);
                        //string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Headers["host"], "/Inicio/Confirmar?token=" + modelo.Token);

                        /*string htmlBody = string.Format(content, modelo.NombreUsuario, url);*/
                        string htmlBody = "Hola Mundo";

                        CorreoDTO correoDTO = new CorreoDTO()
                        {
                            Destinatario = modelo.Correo,
                            Asunto = "Correo confirmacion",
                            Contenido = htmlBody
                        };

                        bool enviado = correo.EnviarCorreo(correoDTO);
                        ViewBag.Creado = true;
                        ViewBag.Mensaje = $"Su cuenta ha sido creada. Hemos enviado un mensaje al correo {modelo.Correo} para confirmar su cuenta";


                        return RedirectToAction("IniciarSesion", "Inicio");
                    }
                    else
                    {
                        ViewData["Mensaje"] = "No se pudo crear el usuario";
                    }
                }
                else
                {
                    ViewData["Mensaje"] = "El email ya se encuentra registrado revise!!";
                }
            }
            else
            {
                ViewBag.Nombre = modelo.NombreUsuario;
                ViewBag.Correo = modelo.Correo;
                ViewData["Mensaje"] = "Las contranseñas no coinciden revise!!";
            }
            return View();
        }

        public ActionResult Confirmar(string token)
        {
            ViewBag.Respuesta = _usuarioServicio.Confirmar(token);
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
            if (usuario_encontrado.Confirmado == false)
            {
                ViewData["Mensaje"] = $"Falta confirmar su cuenta. Sele envio un correo a {correo}";
            }
            else if (usuario_encontrado.Restablecer == false)
            {
                ViewData["Mensaje"] = $"Se ha solicitado restablecer su cuenta, por favor revise su bandeja de correo {correo}";
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
