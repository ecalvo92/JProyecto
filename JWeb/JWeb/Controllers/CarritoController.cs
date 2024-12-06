using JWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace JWeb.Controllers
{
    public class CarritoController : Controller
    {
        private readonly IHttpClientFactory _http;
        private readonly IConfiguration _conf;
        public CarritoController(IHttpClientFactory http, IConfiguration conf)
        {
            _http = http;
            _conf = conf;
        }

        [HttpPost]
        public IActionResult RegistrarCarrito(long ConsecutivoProducto, int Unidades)
        {
            using (var client = _http.CreateClient())
            {
                var ConsecutivoUsuario = long.Parse(HttpContext.Session.GetString("ConsecutivoUsuario")!.ToString());

                var url = _conf.GetSection("Variables:RutaApi").Value + "Carrito/RegistrarCarrito";

                var model = new Carrito();
                model.ConsecutivoUsuario = ConsecutivoUsuario;
                model.ConsecutivoProducto = ConsecutivoProducto;
                model.Unidades = Unidades;

                JsonContent datos = JsonContent.Create(model);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));
                var response = client.PostAsync(url, datos).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                return Json(result!.Codigo);
            }
        }

    }
}
