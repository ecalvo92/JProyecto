using JWeb.Models;
using JWeb.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

namespace JWeb.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IHttpClientFactory _http;
        private readonly IConfiguration _conf;
        private readonly IMetodosComunes _comunes;
        public UsuarioController(IHttpClientFactory http, IConfiguration conf, IMetodosComunes comunes)
        {
            _http = http;
            _conf = conf;
            _comunes = comunes;
        }

        [HttpGet]
        public IActionResult CambiarContrasenna()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CambiarContrasenna(Usuario model)
        {
            model.ContrasennaAnterior = _comunes.Encrypt(model.ContrasennaAnterior);
            model.Contrasenna = _comunes.Encrypt(model.Contrasenna);
            model.ConfirmarContrasenna = _comunes.Encrypt(model.ConfirmarContrasenna);

            if (model.ContrasennaAnterior == model.Contrasenna)
            {
                ViewBag.Mensaje = "Debe ingresar una nueva contraseña";
                return View();
            }
            else if (model.Contrasenna != model.ConfirmarContrasenna)
            {
                ViewBag.Mensaje = "La confirmación de su contraseña no coincide";
                return View();
            }

            model.Consecutivo = long.Parse(HttpContext.Session.GetString("ConsecutivoUsuario")!);

            using (var client = _http.CreateClient())
            {
                string url = _conf.GetSection("Variables:RutaApi").Value + "Usuario/CambiarAcceso";

                JsonContent datos = JsonContent.Create(model);

                var response = client.PutAsync(url, datos).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {
                    return RedirectToAction("CerrarSesion", "Login");
                }
                else
                {
                    ViewBag.Mensaje = result!.Mensaje;
                    return View();
                }
            }
        }

    }
}
