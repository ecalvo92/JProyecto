using JWeb.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;
using System.Drawing;
using System.Net.Http.Headers;
using System.Text.Json;
using JWeb.Servicios;

namespace JWeb.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IHttpClientFactory _http;
        private readonly IConfiguration _conf;

        public ProductoController(IHttpClientFactory http, IConfiguration conf)
        {
            _http = http;
            _conf = conf;
        }

        [HttpGet]
        public IActionResult ConsultarProductos()
        {
            var datos = ObtenerDatosProductos();
            return View(datos);
        }


        [HttpGet]
        public IActionResult RegistrarProducto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarProducto(IFormFile ImagenProducto, Producto model)
        {
            using (var client = _http.CreateClient())
            {
                string url = _conf.GetSection("Variables:RutaApi").Value + "Producto/RegistrarProducto";

                JsonContent datos = JsonContent.Create(model);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));
                var response = client.PostAsync(url, datos).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {
                    return RedirectToAction("ConsultarProductos", "Producto");
                }
                else
                {
                    ViewBag.Mensaje = result!.Mensaje;
                    return View();
                }
            }
        }


        [HttpPost]
        public IActionResult ActualizarEstado(Producto model)
        {
            using (var client = _http.CreateClient())
            {
                string url = _conf.GetSection("Variables:RutaApi").Value + "Producto/ActualizarEstado";

                JsonContent datos = JsonContent.Create(model);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));
                var response = client.PutAsync(url, datos).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {
                    return RedirectToAction("ConsultarProductos", "Producto");
                }
                else
                {
                    ViewBag.Mensaje = result!.Mensaje;
                    var datos2 = ObtenerDatosProductos();
                    return View("ConsultarProductos", datos2);
                }
            }
        }


        private List<Producto> ObtenerDatosProductos()
        {
            using (var client = _http.CreateClient())
            {
                string url = _conf.GetSection("Variables:RutaApi").Value + "Producto/ConsultarProductos";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));
                var response = client.GetAsync(url).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {
                    var datosContenido = JsonSerializer.Deserialize<List<Producto>>((JsonElement)result.Contenido!);
                    return datosContenido!.ToList();
                }

                return new List<Producto>();
            }
        }



    }
}
