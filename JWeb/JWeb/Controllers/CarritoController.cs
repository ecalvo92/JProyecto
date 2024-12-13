using JWeb.Models;
using JWeb.Servicios;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using static NuGet.Packaging.PackagingConstants;
using static System.Net.Mime.MediaTypeNames;

namespace JWeb.Controllers
{
    public class CarritoController : Controller
    {
        private readonly IHttpClientFactory _http;
        private readonly IConfiguration _conf;
        private readonly IMetodosComunes _comunes;
        public CarritoController(IHttpClientFactory http, IConfiguration conf, IMetodosComunes comunes)
        {
            _http = http;
            _conf = conf;
            _comunes = comunes;
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

                var carrito = _comunes.ConsultarCarritoServicio();
                HttpContext.Session.SetString("Total", carrito.Sum(x => x.Total).ToString());

                return Json(result!.Codigo);
            }
        }

        [HttpGet]
        public IActionResult ConsultarCarrito()
        {
            return View(_comunes.ConsultarCarritoServicio());
        }

        [HttpGet]
        public IActionResult ConsultarFacturas()
        {
            using (var client = _http.CreateClient())
            {
                var ConsecutivoUsuario = long.Parse(HttpContext.Session.GetString("ConsecutivoUsuario")!.ToString());

                string url = _conf.GetSection("Variables:RutaApi").Value + "Carrito/ConsultarFacturas?Consecutivo=" + ConsecutivoUsuario;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));
                var response = client.GetAsync(url).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {
                    var datosContenido = JsonSerializer.Deserialize<List<Carrito>>((JsonElement)result.Contenido!);
                    return View(datosContenido!.ToList());
                }

                return View(new List<Carrito>());
            }
        }

        [HttpGet]
        public IActionResult ConsultarDetalleFactura(long consecutivo)
        {
            using (var client = _http.CreateClient())
            {
                string url = _conf.GetSection("Variables:RutaApi").Value + "Carrito/ConsultarDetalleFactura?Consecutivo=" + consecutivo;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));
                var response = client.GetAsync(url).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {
                    var datosContenido = JsonSerializer.Deserialize<List<Carrito>>((JsonElement)result.Contenido!);
                    return View(datosContenido!.ToList());
                }

                return View(new List<Carrito>());
            }
        }

        [HttpPost]
        public IActionResult RemoverProductoCarrito(Carrito model)
        {
            using (var client = _http.CreateClient())
            {
                var url = _conf.GetSection("Variables:RutaApi").Value + "Carrito/RemoverProductoCarrito";

                model.ConsecutivoUsuario = long.Parse(HttpContext.Session.GetString("ConsecutivoUsuario")!.ToString());

                JsonContent datos = JsonContent.Create(model);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));
                var response = client.PostAsync(url, datos).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {
                    var carrito = _comunes.ConsultarCarritoServicio();
                    HttpContext.Session.SetString("Total", carrito.Sum(x => x.Total).ToString());

                    return RedirectToAction("ConsultarCarrito", "Carrito");
                }
                else
                {
                    ViewBag.Mensaje = result!.Mensaje;
                    return View("ConsultarCarrito", _comunes.ConsultarCarritoServicio());
                }
            }
        }

        [HttpPost]
        public IActionResult PagarCarrito(Carrito model)
        {
            using (var client = _http.CreateClient())
            {
                var url = _conf.GetSection("Variables:RutaApi").Value + "Carrito/PagarCarrito";

                model.ConsecutivoUsuario = long.Parse(HttpContext.Session.GetString("ConsecutivoUsuario")!.ToString());

                JsonContent datos = JsonContent.Create(model);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));
                var response = client.PostAsync(url, datos).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {
                    var carrito = _comunes.ConsultarCarritoServicio();
                    HttpContext.Session.SetString("Total", carrito.Sum(x => x.Total).ToString());

                    return RedirectToAction("Inicio", "Home");
                }
                else
                {
                    ViewBag.Mensaje = result!.Mensaje;
                    return View("ConsultarCarrito", _comunes.ConsultarCarritoServicio());
                }
            }
        }
    }
}
