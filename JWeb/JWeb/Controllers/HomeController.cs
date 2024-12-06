using JWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static System.Net.WebRequestMethods;
using System.Drawing;
using System.Net.Http.Headers;
using System.Text.Json;

namespace JWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _http;
        private readonly IConfiguration _conf;
        public HomeController(IHttpClientFactory http, IConfiguration conf)
        {
            _http = http;
            _conf = conf;
        }

        [HttpGet]
        public IActionResult Inicio()
        {
            using (var client = _http.CreateClient())
            {
                string url = _conf.GetSection("Variables:RutaApi").Value + "Producto/ConsultarProductos";

                var response = client.GetAsync(url).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {
                    var datosContenido = JsonSerializer.Deserialize<List<Producto>>((JsonElement)result.Contenido!);
                    return View(datosContenido!.Where(x => x.Inventario > 0 && x.Estado == "Activo").ToList());
                }

                return View(new List<Producto>());
            }

        }
    }
}
