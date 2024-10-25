using JWeb.Models;
using JWeb.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace JWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _http;
        private readonly IConfiguration _conf;
        private readonly IMetodosComunes _comunes;
        public LoginController(IHttpClientFactory http, IConfiguration conf, IMetodosComunes comunes)
        {
            _http = http;
            _conf = conf;
            _comunes = comunes;
        }

        [HttpGet]
        public IActionResult CrearCuenta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearCuenta(Usuario model)
        {
            using (var client = _http.CreateClient())
            {
                string url = _conf.GetSection("Variables:RutaApi").Value + "Login/CrearCuenta";

                model.Contrasenna = _comunes.Encrypt(model.Contrasenna);
                JsonContent datos = JsonContent.Create(model);

                var response = client.PostAsync(url, datos).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {
                    return RedirectToAction("IniciarSesion", "Login");
                }
                else
                {
                    ViewBag.Mensaje = result!.Mensaje;
                    return View();
                }
            }
        }



        [HttpGet]
        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IniciarSesion(Usuario model)
        {
            using (var client = _http.CreateClient())
            {
                string url = _conf.GetSection("Variables:RutaApi").Value + "Login/IniciarSesion";

                model.Contrasenna = _comunes.Encrypt(model.Contrasenna);
                JsonContent datos = JsonContent.Create(model);

                var response = client.PostAsync(url, datos).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {
                    var datosContenido = JsonSerializer.Deserialize<Usuario>((JsonElement)result.Contenido!);

                    HttpContext.Session.SetString("ConsecutivoUsuario", datosContenido!.Consecutivo.ToString());
                    HttpContext.Session.SetString("NombreUsuario", datosContenido!.Nombre);

                    return RedirectToAction("Inicio", "Home");
                }
                else
                {
                    ViewBag.Mensaje = result!.Mensaje;
                    return View();
                }
            }
        }



        [HttpGet]
        public IActionResult RecuperarAcceso()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecuperarAcceso(Usuario model)
        {
            using (var client = _http.CreateClient())
            {
                string url = _conf.GetSection("Variables:RutaApi").Value + "Login/RecuperarAcceso";

                JsonContent datos = JsonContent.Create(model);

                var response = client.PostAsync(url, datos).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {
                    return RedirectToAction("IniciarSesion", "Login");
                }
                else
                {
                    ViewBag.Mensaje = result!.Mensaje;
                    return View();
                }
            }
        }



        [HttpGet]
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Inicio", "Home");
        }


    }
}
