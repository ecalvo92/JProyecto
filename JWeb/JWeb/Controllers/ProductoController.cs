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
        private readonly IHostEnvironment _env;

        public ProductoController(IHttpClientFactory http, IConfiguration conf, IHostEnvironment env)
        {
            _http = http;
            _conf = conf;
            _env = env;
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
            var ext = string.Empty;
            var folder = string.Empty;
            model.Imagen = string.Empty;

            if (ImagenProducto != null)
            {
                ext = Path.GetExtension(Path.GetFileName(ImagenProducto.FileName));
                folder = Path.Combine(_env.ContentRootPath, "wwwroot\\products");
                model.Imagen = "/products/";

                if (ext.ToLower() != ".png")
                {
                    ViewBag.Mensaje = "La imagen debe ser .png";
                    return View();
                }
            }

            using (var client = _http.CreateClient())
            {
                string url = _conf.GetSection("Variables:RutaApi").Value + "Producto/RegistrarProducto";

                JsonContent datos = JsonContent.Create(model);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));
                var response = client.PostAsync(url, datos).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {
                    if (ImagenProducto != null)
                    {
                        string archivo = Path.Combine(folder, result.Mensaje + ext);
                        using (Stream fs = new FileStream(archivo, FileMode.Create))
                        {
                            ImagenProducto.CopyTo(fs);
                        }
                    }

                    return RedirectToAction("ConsultarProductos", "Producto");
                }
                else
                {
                    ViewBag.Mensaje = result!.Mensaje;
                    return View();
                }
            }
        }


        [HttpGet]
        public IActionResult ActualizarProducto(long consecutivo)
        {
            using (var client = _http.CreateClient())
            {
                string url = _conf.GetSection("Variables:RutaApi").Value + "Producto/ConsultarProducto?Consecutivo=" + consecutivo;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));
                var response = client.GetAsync(url).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {
                    var datosContenido = JsonSerializer.Deserialize<Producto>((JsonElement)result.Contenido!);
                    return View(datosContenido);
                }

                return View(new Producto());
            }
        }

        [HttpPost]
        public IActionResult ActualizarProducto(IFormFile ImagenProducto, Producto model)
        {
            var ext = string.Empty;
            var folder = string.Empty;

            if (ImagenProducto != null)
            {
                ext = Path.GetExtension(Path.GetFileName(ImagenProducto.FileName));
                folder = Path.Combine(_env.ContentRootPath, "wwwroot\\products");

                if (ext.ToLower() != ".png")
                {
                    ViewBag.Mensaje = "La imagen debe ser .png";
                    return View();
                }
            }

            using (var client = _http.CreateClient())
            {
                string url = _conf.GetSection("Variables:RutaApi").Value + "Producto/ActualizarProducto";

                JsonContent datos = JsonContent.Create(model);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("TokenUsuario"));
                var response = client.PutAsync(url, datos).Result;
                var result = response.Content.ReadFromJsonAsync<Respuesta>().Result;

                if (result != null && result.Codigo == 0)
                {
                    if (ImagenProducto != null)
                    {
                        string archivo = Path.Combine(folder, model.Consecutivo + ext);
                        using (Stream fs = new FileStream(archivo, FileMode.Create))
                        {
                            ImagenProducto.CopyTo(fs);
                        }
                    }

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
